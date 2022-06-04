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

        private readonly BinanceSocketClient _binanceClient = new BinanceSocketClient();

        private readonly Dictionary<SpotOpenOrdersSubscriber, SpotOpenOrdersSubscribeData> _subscriptions =
            new Dictionary<SpotOpenOrdersSubscriber, SpotOpenOrdersSubscribeData>();

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

            var listener = new BinanceClient();
            listener.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);
            WebCallResult<string>? listenKeyResult = await listener.Spot.UserStream.StartUserStreamAsync();

            if (!listenKeyResult.Success)
                throw new InvalidOperationException("Cannot subscribe");

            var subscriberData = new SpotOpenOrdersSubscribeData
            {
                Client = listener,
                ListenerKey = listenKeyResult.Data,
                SocketClient = new BinanceSocketClient()
            };
            subscriberData.SocketClient.SetApiCredentials(subscriber.Token.Key, subscriber.Token.Secret);

            await subscriberData.SocketClient.Spot.SubscribeToUserDataUpdatesAsync(subscriberData.ListenerKey,
                (orderUpdate) => OnOrderUpdate?.Invoke(this, orderUpdate.Data),
                (ocoOrderUpdate) => OnOcoOrderUpdate?.Invoke(this, ocoOrderUpdate.Data),
                (positionUpdate) => OnAccountPositionUpdate?.Invoke(this, positionUpdate.Data),
                (balanceUpdate) => OnAccountBalanceUpdate?.Invoke(this, balanceUpdate.Data));

            _subscriptions.Add(subscriber, subscriberData);
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
