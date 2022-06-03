using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Broadcast;
    using Clients;

    /// <summary>
    /// SignalR Hub for subscribing to a binance spot depth market updates <para/>
    /// Inherited from <see cref="Hub"/>
    /// </summary>
    [SignalRHub("api/binance/spot/realtime/depth-market")]
    public class BinanceSpotDepthMarketHub : SubscriberHub<BinanceSpotDepthMarketHub, 
                                                           IBinanceSpotDepthMarketHubClient, 
                                                           BinanceSpotDepthMarketService>
    { }
}
