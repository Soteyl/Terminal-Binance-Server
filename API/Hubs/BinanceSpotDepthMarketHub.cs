using Ixcent.CryptoTerminal.Api.Hubs.Broadcast;
using Ixcent.CryptoTerminal.Api.Hubs.Clients;

using Microsoft.AspNetCore.Authorization;

using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    /// <summary>
    /// SignalR Hub for subscribing to a binance spot depth market updates <para/>
    /// </summary>
    /// <remarks>
    /// Inherited from <see cref="SubscriberHub{THub, THubClient, THubSubscriberService}"/> <br/>
    /// <c>THub</c> is <see cref="BinanceSpotDepthMarketHub"/> <br/>
    /// <c>THubClient</c> is <see cref="IBinanceSpotDepthMarketHubClient"/> <br/>
    /// <c>THubSubscriberService</c> is <see cref="BinanceSpotDepthMarketService"/> <br/>
    /// Url: <c>api/binance/spot/realtime/depth-market</c>
    /// </remarks>
    [Authorize]
    [SignalRHub("api/binance/spot/realtime/depth-market")]
    public class BinanceSpotDepthMarketHub : SubscriberHub<BinanceSpotDepthMarketHub,
                                                           IBinanceSpotDepthMarketHubClient,
                                                           BinanceSpotDepthMarketService>
    {
        /// <summary>
        /// Method for clients who want to subscribe to a symbol updates
        /// </summary>
        /// <param name="symbol">symbol pair, like BTCUSDT</param>
        public override Task Subscribe(string symbol)
        {
            return base.Subscribe(symbol);
        }

        /// <summary>
        /// Method for clients who want to unsubscribe to a symbol updates
        /// </summary>
        /// <param name="symbol">symbol pair, like BTCUSDT</param>
        public override Task Unsubscribe(string symbol)
        {
            return base.Unsubscribe(symbol);
        }
    }
}
