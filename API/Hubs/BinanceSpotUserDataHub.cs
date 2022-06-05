using Ixcent.CryptoTerminal.Api.Hubs.Broadcast;
using Ixcent.CryptoTerminal.Api.Hubs.Clients;

using Microsoft.AspNetCore.Authorization;

using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    /// <summary>
    /// Subscriber hub for receiving binance spot account user data updates
    /// </summary>
    /// <remarks>
    /// Inherited from <see cref="SubscriberHubBase{THub, THubClient, THubSubscriberService, TData}"/> <br/>
    /// <c>THub</c> is <see cref="BinanceSpotUserDataHub"/> <br/>
    /// <c>THubClient</c> is <see cref="IBinanceSpotUserDataHubClient"/> <br/>
    /// <c>THubSubscriberService</c> is <see cref="BinanceSpotUserDataService"/> <br/>
    /// <c>TData</c> is <see cref="object"/>? <br/>
    /// </remarks>
    [Authorize]
    [SignalRHub("api/binance/spot/realtime/open-orders")]
    public class BinanceSpotUserDataHub :
        SubscriberHubBase<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient, BinanceSpotUserDataService, object?>
    {
        /// <summary>
        /// Method for subscribing to account info
        /// </summary>
        public virtual async Task Subscribe()
        {
            await Service.Subscribe(null, Context.ConnectionId, Context);
        }

        /// <summary>
        /// Method for unsubscribing to account info
        /// </summary>
        public virtual async Task Unsubscribe()
        {
            await Service.Unsubscribe(Context.ConnectionId, Context);
        }
    }
}
