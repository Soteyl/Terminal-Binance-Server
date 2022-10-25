using System.Diagnostics.CodeAnalysis;

using Binance.Net;
using Binance.Net.Objects.Spot.UserStream;

using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

using Ixcent.CryptoTerminal.Domain.Common.Extensions;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime
{
    /// <summary>
    /// Singleton uploader account data for subscribers.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IDisposable"/>
    /// </remarks>
    public class UserData : IDisposable
    {
        private static UserData _instance;

        private readonly CancellationTokenSource _keepAliveCancellationToken;

        private readonly Dictionary<string, SpotOpenOrdersSubscribeData> _subscriptions = new();

        private UserData()
        {
            _keepAliveCancellationToken = new CancellationTokenSource();
#pragma warning disable CS4014
            KeepAliveListeners(_keepAliveCancellationToken.Token);
#pragma warning restore CS4014
        }

        /// <summary>
        /// Returns a single instance of a class
        /// </summary>
        public static UserData GetInstance()
        {
            if (_instance == null)
                _instance = new UserData();
            return _instance;
        }

        /// <summary>
        /// Subscribes to a receiving userdata from binance spot account. 
        /// Data sends via events. For receiving this data - subscribe for concrete event.
        /// </summary>
        /// <param name="subscriber">Subscriber data</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Task Subscribe([NotNull] SpotOpenOrdersSubscriber subscriber)
        {
            if (subscriber == null
                || string.IsNullOrWhiteSpace(subscriber.ConnectionId)
                || subscriber.Token == null
                || string.IsNullOrWhiteSpace(subscriber.Token.Key)
                || string.IsNullOrWhiteSpace(subscriber.Token.Secret))
                throw new ArgumentException("Subscriber must not be null, must contain not nullable connectionId and ExchangeToken");

            lock (_subscriptions)
            {

                if (_subscriptions.ContainsKey(subscriber.ConnectionId))
                {
                    Unsubscribe(subscriber.ConnectionId).Wait();
                }

                BinanceClient? listener = new();
                listener.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);
                WebCallResult<string>? listenKeyResult = listener.Spot.UserStream.StartUserStreamAsync().WaitThis().Result;

                if (!listenKeyResult.Success)
                    throw new InvalidOperationException("Cannot subscribe");

                SpotOpenOrdersSubscribeData? subscriberData = new()
                {
                    Client = listener,
                    ListenerKey = listenKeyResult.Data,
                    SocketClient = new BinanceSocketClient()
                };
                subscriberData.SocketClient.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);

                CallResult<UpdateSubscription>? result = subscriberData.SocketClient.Spot.SubscribeToUserDataUpdatesAsync(subscriberData.ListenerKey,
                    (orderUpdate) => OnOrderUpdate?.Invoke(subscriber.ConnectionId, orderUpdate.Data),
                    (ocoOrderUpdate) => OnOcoOrderUpdate?.Invoke(subscriber.ConnectionId, ocoOrderUpdate.Data),
                    (positionUpdate) => OnAccountPositionUpdate?.Invoke(subscriber.ConnectionId, positionUpdate.Data),
                    (balanceUpdate) => OnAccountBalanceUpdate?.Invoke(subscriber.ConnectionId, balanceUpdate.Data)).WaitThis().Result;

                _subscriptions[subscriber.ConnectionId] = subscriberData;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Unsubscribes from receiving user data
        /// </summary>
        /// <param name="connectionId">connection id of user to unsubscribe</param>
        public async Task Unsubscribe(string connectionId)
        {
            KeyValuePair<string, SpotOpenOrdersSubscribeData> subscriber = _subscriptions.FirstOrDefault(sub => sub.Key.Equals(connectionId));
            if (subscriber.Key == null)
                return;

            await subscriber.Value.SocketClient.UnsubscribeAllAsync();
            subscriber.Value.SocketClient.Dispose();
            await subscriber.Value.Client.Spot.UserStream.StopUserStreamAsync(subscriber.Value.ListenerKey);
            subscriber.Value.Client.Dispose();

            _subscriptions.Remove(subscriber.Key);
        }

        public void Dispose()
        {
            _keepAliveCancellationToken.Cancel();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Invokes when there are changes with any spot order in your account
        /// </summary>
        public event EventHandler<BinanceStreamOrderUpdate> OnOrderUpdate;

        /// <summary>
        /// Invokes when there are changes with any spot OCO order in your account
        /// </summary>
        public event EventHandler<BinanceStreamOrderList> OnOcoOrderUpdate;

        /// <summary>
        /// Invokes on position update (check return object class)
        /// </summary>
        public event EventHandler<BinanceStreamPositionsUpdate> OnAccountPositionUpdate;

        /// <summary>
        /// Invokes on account balance update
        /// </summary>
        public event EventHandler<BinanceStreamBalanceUpdate> OnAccountBalanceUpdate;

        private async Task KeepAliveListeners(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                foreach (SpotOpenOrdersSubscribeData listener in _subscriptions.Values)
                {
                    await listener.Client.Spot.UserStream.KeepAliveUserStreamAsync(listener.ListenerKey, CancellationToken.None);
                }
                // listen stops after 60 mins
                await Task.Delay(TimeSpan.FromMinutes(50), CancellationToken.None);
            }
        }

        private class SpotOpenOrdersSubscribeData
        {
            public BinanceSocketClient SocketClient { get; set; }

            public BinanceClient Client { get; set; }

            public string ListenerKey { get; set; }
        }
    }

    /// <summary>
    /// Subscriber data
    /// </summary>
    public class SpotOpenOrdersSubscriber
    {
        public string ConnectionId { get; set; }

        public ExchangeTokenEntity Token { get; set; }
    }
}
