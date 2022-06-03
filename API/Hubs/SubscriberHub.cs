namespace Ixcent.CryptoTerminal.Api.Hubs
{
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
        connection.invoke("Subscribe", "BTCUSDT");
     */
    #endregion

    /// <summary>
    /// Base class for subscriber hubs
    /// </summary>
    /// <typeparam name="THub">Current class, where is SubscriberHub inherited</typeparam>
    /// <typeparam name="THubClient">Some client</typeparam>
    /// <typeparam name="THubSubscriberService">Subscriber service for this hub</typeparam>
    public abstract class SubscriberHub<THub, THubClient, THubSubscriberService>
            : SubscriberHubBase<THub, THubClient, THubSubscriberService, object?>
        where THubSubscriberService : SubscriberHubService<THub, THubClient, object?>, new()
        where THub : SubscriberHubBase<THub, THubClient, THubSubscriberService, object?>
        where THubClient : class
    {
        /// <summary>
        /// Method for subscribing to a group
        /// </summary>
        /// <param name="groupName">Name of group</param>
        public virtual async Task Subscribe(string groupName)
        {
            await Service.Subscribe(null, groupName, Context);
        }

        /// <summary>
        /// Method for unsubscribing to a group
        /// </summary>
        /// <param name="groupName">Name of group</param>
        public virtual async Task Unsubscribe(string groupName)
        {
            await Service.Unsubscribe(groupName, Context);
        }
    }

    /// <summary>
    /// Base class for subscriber hubs
    /// </summary>
    /// <typeparam name="THub">Current class, where is SubscriberHub inherited</typeparam>
    /// <typeparam name="THubClient">Some client</typeparam>
    /// <typeparam name="THubSubscriberService">Subscriber service for this hub</typeparam>
    /// <typeparam name="TData">Additional data for subscribing methods</typeparam>
    public abstract class SubscriberHub<THub, THubClient, THubSubscriberService, TData>
            : SubscriberHubBase<THub, THubClient, THubSubscriberService, TData>
        where THubSubscriberService : SubscriberHubService<THub, THubClient, TData>, new()
        where THub : SubscriberHubBase<THub, THubClient, THubSubscriberService, TData>
        where THubClient : class
    {
        /// <summary>
        /// Method for subscribing to a group
        /// </summary>
        /// <param name="data">Custom data</param>
        /// <param name="groupName">Name of group</param>
        public virtual async Task Subscribe(TData data, string groupName)
        {
            await Service.Subscribe(data, groupName, Context);
        }

        /// <summary>
        /// Method for unsubscribing to a group
        /// </summary>
        /// <param name="data">Custom data</param>
        /// <param name="groupName">Name of group</param>
        public virtual async Task Unsubscribe(TData data, string groupName)
        {
            await Service.Subscribe(data, groupName, Context);
        }
    }
}
