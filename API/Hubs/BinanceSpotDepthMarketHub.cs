using Microsoft.AspNetCore.SignalR;
using Binance.Net.Interfaces;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Application.Exchanges.Binance;
    using Infrastructure;

    public interface IBinanceSpotDepthMarketHubClient
    {
        Task ReceiveDepthMarketUpdate(IBinanceOrderBook orderBook);
    }

    /// <summary>
    /// SignalR Hub for subscribing to a binance spot depth market updates <para/>
    /// Inherited from <see cref="Hub"/>
    /// </summary>
    public class BinanceSpotDepthMarketHub : Hub<IBinanceSpotDepthMarketHubClient>
    {
        private readonly RealtimeSpotDepthMarket _realtimeMarket = new RealtimeSpotDepthMarket();

        private readonly ConnectionMapping<string> _subscribers = new ConnectionMapping<string>();

        public BinanceSpotDepthMarketHub()
        {
            _subscribers.OnKeyEmpty += RemoveKey;
            _realtimeMarket.DepthMarketUpdated += NotifyAboutMarketUpdates;
        }

        /// <summary>
        /// Method for clients who want to subscribe to a symbol updates
        /// </summary>
        /// <param name="symbol"></param>
        public void SubscribeToSymbol(string symbol)
        {
            _subscribers.Add(symbol, Context.ConnectionId);
            _realtimeMarket.SubscribeTo(symbol);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _subscribers.RemoveByConnectionId(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            _subscribers.OnKeyEmpty -= RemoveKey;
            base.Dispose(disposing);
        }

        private void RemoveKey(string key)
        {
            _realtimeMarket.UnsubscribeFrom(key);
        }

        private void NotifyAboutMarketUpdates(object? sender, IBinanceOrderBook e)
        {
            foreach(string? connection in _subscribers.GetConnections(e.Symbol))
            {
                Clients.Client(connection).ReceiveDepthMarketUpdate(e);
            }
        }
    }
}
