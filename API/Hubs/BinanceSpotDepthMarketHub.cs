using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Broadcast;
    using Clients;
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR Hub for subscribing to a binance spot depth market updates <para/>
    /// Inherited from <see cref="Hub"/>
    /// </summary>
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
