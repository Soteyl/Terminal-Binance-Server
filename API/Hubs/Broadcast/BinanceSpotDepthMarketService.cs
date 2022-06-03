using Microsoft.AspNetCore.SignalR;
using Binance.Net.Interfaces;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    using Application.Exchanges.Binance;
    using Clients;

    /// <summary> Receives updates from Binance spot market for Depth and sends it through the clients who subscribed. </summary>
    /// <remarks> Implements <see cref="IDisposable"/> </remarks>
    public class BinanceSpotDepthMarketService
        : SubscriberHubService<BinanceSpotDepthMarketHub, IBinanceSpotDepthMarketHubClient, object?>
    {
        private readonly RealtimeSpotDepthMarket _realtimeMarket = new RealtimeSpotDepthMarket();

        public BinanceSpotDepthMarketService()
        {
            _realtimeMarket.DepthMarketUpdated += NotifyAboutMarketUpdates;
        }

        public override async Task Subscribe(object? data, string groupName, HubCallerContext context)
        {
            await base.Subscribe(data, groupName, context);
            _realtimeMarket.SubscribeTo(groupName);
        }

        protected override void SubscriberKeyEmpty(string key)
        {
            _realtimeMarket.UnsubscribeFrom(key);
            base.SubscriberKeyEmpty(key);
        }

        public Task UnsubscribeFromAll(string connectionId)
        {
            _subscribers.RemoveByConnectionId(connectionId);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _realtimeMarket.DepthMarketUpdated -= NotifyAboutMarketUpdates;
            base.Dispose();
        }

        private void NotifyAboutMarketUpdates(object? sender, IBinanceOrderBook e)
        {
            _hubContext.Clients.Group(e.Symbol).ReceiveDepthMarketUpdate(e);
        }
    }
}
