using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Broadcast;
    using Clients;

    #region Test in browser
    /* var sc = document.createElement("script");
        sc.setAttribute("src", "https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/3.1.7/signalr.min.js");
        sc.setAttribute("type", "text/javascript");
        document.head.appendChild(sc);

        var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7145/api/binance/spot/realtime/depth-market", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJuYW1laWQiOiJCaW1iYSIsIm5iZiI6MTY1MzM5MDczOSwiZXhwIjoxNjUzOTk1NTM5LCJpYXQiOjE2NTMzOTA3Mzl9.dUMHlRHceMYR4PvJUrHiNqDRoKxvaxAjRWH_wnp_vb282A9wcHBVUVLN9CBwJQAFsaljRNkK7kCgpWLn4G2DQg"
        })
        .build();

        connection.on("ReceiveDepthMarketUpdate", function (e) { console.log({e}); });
        connection.start();
        connection.invoke("SubscribeToSymbol", "BTCUSDT");
     */
    #endregion
    /// <summary>
    /// SignalR Hub for subscribing to a binance spot depth market updates <para/>
    /// Inherited from <see cref="Hub"/>
    /// </summary>
    [SignalRHub("api/binance/spot/realtime/depth-market")]
    public class BinanceSpotDepthMarketHub : Hub<IBinanceSpotDepthMarketHubClient>
    {
        #region Updater
        private static BinanceSpotDepthMarketUpdater __updater;

        private BinanceSpotDepthMarketUpdater Updater
        {
            get
            {
                if (__updater == null)
                {
                    var hubContext = Context.GetHttpContext().RequestServices.GetService
                        <IHubContext<BinanceSpotDepthMarketHub, IBinanceSpotDepthMarketHubClient>>();
                    __updater = new BinanceSpotDepthMarketUpdater(hubContext);
                }

                return __updater;
            }
        }
        #endregion

        /// <summary>
        /// Method for clients who want to subscribe to a symbol updates
        /// </summary>
        /// <param name="symbol">symbol pair, like BTCUSDT</param>
        public async Task SubscribeToSymbol(string symbol)
        {
            await Updater.SubscribeToSymbol(symbol, Context.ConnectionId);
        }

        /// <summary>
        /// Method for clients who want to unsubscribe to a symbol updates
        /// </summary>
        /// <param name="symbol">symbol pair, like BTCUSDT</param>
        public async Task UnsubscribeFromSymbol(string symbol)
        {
            await Updater.UnsubscribeFromSymbol(symbol, Context.ConnectionId);
        }

        /// <summary> Unsubscribes from all subscribed updates. </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Updater.UnsubscribeFromAll(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
