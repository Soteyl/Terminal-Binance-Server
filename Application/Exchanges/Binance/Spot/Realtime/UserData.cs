using Binance.Net.Objects.Spot.UserStream;
using System.Diagnostics.CodeAnalysis;
using CryptoExchange.Net.Objects;
using Binance.Net;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime
{
    using Domain.Database.Models;

    public class UserData : IDisposable
    {
        private static UserData _instance;

        private readonly CancellationTokenSource _keepAliveCancellationToken;

        private readonly Dictionary<string, SpotOpenOrdersSubscribeData> _subscriptions =
            new Dictionary<string, SpotOpenOrdersSubscribeData>();

        private UserData()
        {
            _keepAliveCancellationToken = new CancellationTokenSource();
            KeepAliveListeners(_keepAliveCancellationToken.Token);
        }

        public static UserData GetInstance()
        {
            if (_instance == null)
                _instance = new UserData();
            return _instance;
        }

        public async Task Subscribe([NotNull] SpotOpenOrdersSubscriber subscriber)
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
                    Unsubscribe(subscriber.ConnectionId);
                }

                var listener = new BinanceClient();
                listener.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);
                WebCallResult<string>? listenKeyResult = listener.Spot.UserStream.StartUserStreamAsync().WaitThis().Result;

                if (!listenKeyResult.Success)
                    throw new InvalidOperationException("Cannot subscribe");

                var subscriberData = new SpotOpenOrdersSubscribeData
                {
                    Client = listener,
                    ListenerKey = listenKeyResult.Data,
                    SocketClient = new BinanceSocketClient()
                };
                subscriberData.SocketClient.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);

                var result = subscriberData.SocketClient.Spot.SubscribeToUserDataUpdatesAsync(subscriberData.ListenerKey,
                    (orderUpdate) => OnOrderUpdate?.Invoke(subscriber.ConnectionId, orderUpdate.Data),
                    (ocoOrderUpdate) => OnOcoOrderUpdate?.Invoke(subscriber.ConnectionId, ocoOrderUpdate.Data),
                    (positionUpdate) => OnAccountPositionUpdate?.Invoke(subscriber.ConnectionId, positionUpdate.Data),
                    (balanceUpdate) => OnAccountBalanceUpdate?.Invoke(subscriber.ConnectionId, balanceUpdate.Data)).WaitThis().Result;

                _subscriptions[subscriber.ConnectionId] = subscriberData;
            }
        }

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
        }

        public event EventHandler<BinanceStreamOrderUpdate> OnOrderUpdate;

        public event EventHandler<BinanceStreamOrderList> OnOcoOrderUpdate;

        public event EventHandler<BinanceStreamPositionsUpdate> OnAccountPositionUpdate;

        public event EventHandler<BinanceStreamBalanceUpdate> OnAccountBalanceUpdate;

        private async Task KeepAliveListeners(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                foreach (SpotOpenOrdersSubscribeData listener in _subscriptions.Values)
                {
                    await listener.Client.Spot.UserStream.KeepAliveUserStreamAsync(listener.ListenerKey);
                }
                // listen stops after 60 mins
                await Task.Delay(TimeSpan.FromMinutes(50));
            }
        }

        private class SpotOpenOrdersSubscribeData
        {
            public BinanceSocketClient SocketClient { get; set; }

            public BinanceClient Client { get; set; }

            public string ListenerKey { get; set; }
        }
    }

    public class SpotOpenOrdersSubscriber
    {
        public string ConnectionId { get; set; }

        public ExchangeToken Token { get; set; }
    }
}
