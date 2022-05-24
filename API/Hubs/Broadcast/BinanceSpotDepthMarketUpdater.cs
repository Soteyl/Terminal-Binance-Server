using Binance.Net.Interfaces;

using IBinanceSpotDepthMarketHubContext = 
    Microsoft.AspNetCore.SignalR.IHubContext<
        Ixcent.CryptoTerminal.Api.Hubs.BinanceSpotDepthMarketHub, 
        Ixcent.CryptoTerminal.Api.Hubs.Clients.IBinanceSpotDepthMarketHubClient>;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    using Application.Exchanges.Binance;
    using Infrastructure;

    /// <summary> Receives updates from Binance spot market for Depth and sends it through the clients who subscribed. </summary>
    /// <remarks> Implements <see cref="IDisposable"/> </remarks>
    public class BinanceSpotDepthMarketUpdater: IDisposable
    {
        private readonly IBinanceSpotDepthMarketHubContext _hubContext;

        private readonly RealtimeSpotDepthMarket _realtimeMarket = new RealtimeSpotDepthMarket();

        private readonly ConnectionMapping<string> _subscribers = new ConnectionMapping<string>();

        /// <param name="hubContext">Context of a Binance spot depth market subscribers</param>
        public BinanceSpotDepthMarketUpdater(IBinanceSpotDepthMarketHubContext hubContext)
        {
            _hubContext = hubContext;
            _subscribers.OnKeyEmpty += RemoveKey;
            _realtimeMarket.DepthMarketUpdated += NotifyAboutMarketUpdates;
        }

        /// <summary> Subscribes a client to depth market updates </summary>
        /// <param name="symbol">Symbol pair like BTCUSDT</param>
        /// <param name="connectionId">Connection Id from hub context</param>
        public async Task SubscribeToSymbol(string symbol, string connectionId)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, symbol.ToUpper());
            _subscribers.Add(symbol, connectionId);
            _realtimeMarket.SubscribeTo(symbol);
        }

        /// <summary> Unubscribes a client to depth market updates </summary>
        /// <param name="symbol">Symbol pair like BTCUSDT</param>
        /// <param name="connectionId">Connection Id from hub context</param>
        public async Task UnsubscribeFromSymbol(string symbol, string connectionId)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, symbol.ToUpper());
            _subscribers.Remove(symbol, connectionId);
        }

        /// <summary>
        /// Unsubscribes from accepting 
        /// </summary>
        public void Dispose()
        {
            _subscribers.OnKeyEmpty -= RemoveKey;
            _realtimeMarket.DepthMarketUpdated -= NotifyAboutMarketUpdates;
        }

        private void RemoveKey(string key)
        {
            _realtimeMarket.UnsubscribeFrom(key);
        }

        private void NotifyAboutMarketUpdates(object? sender, IBinanceOrderBook e)
        {
            foreach (string? connection in _subscribers.GetConnections(e.Symbol))
            {
                _hubContext.Clients.Client(connection).ReceiveDepthMarketUpdate(e);
            }
        }
    }
}
