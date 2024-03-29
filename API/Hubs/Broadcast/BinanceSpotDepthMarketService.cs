﻿using Binance.Net.Interfaces;

using Ixcent.CryptoTerminal.Api.Hubs.Clients;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime;

using Microsoft.AspNetCore.SignalR;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    /// <summary> Receives updates from Binance spot market for Depth and sends it through the clients who subscribed. </summary>
    /// <remarks> Implements <see cref="IDisposable"/> </remarks>
    public class BinanceSpotDepthMarketService
        : SubscriberHubService<BinanceSpotDepthMarketHub, IBinanceSpotDepthMarketHubClient>
    {
        private readonly DepthMarket _realtimeMarket = new DepthMarket();

        public override void AddServiceProvider(IServiceProvider provider)
        {
            base.AddServiceProvider(provider);
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
            GC.SuppressFinalize(this);
        }

        private void NotifyAboutMarketUpdates(object? sender, IBinanceOrderBook e)
        {
            _hubContext.Clients.Group(e.Symbol).ReceiveDepthMarketUpdate(e);
        }
    }
}
