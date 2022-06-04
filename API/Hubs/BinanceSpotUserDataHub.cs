using Microsoft.AspNetCore.Authorization;
using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Broadcast;
    using Clients;

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
