using Microsoft.AspNetCore.SignalR;

using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    /// <summary>
    /// Base subscriber hub class, containing singleton service
    /// </summary>
    /// <typeparam name="THub">Current class, where is SubscriberHub inherited</typeparam>
    /// <typeparam name="THubClient">Some client</typeparam>
    /// <typeparam name="THubSubscriberService">Subscriber service for this hub</typeparam>
    /// <typeparam name="TData">Additional data for subscribing methods</typeparam>
    [SignalRHidden]
    [SignalRHub()]
    public abstract class SubscriberHubBase<THub, THubClient, THubSubscriberService, TData> : Hub<THubClient>
        where THubSubscriberService : ISubscriberHubService<THub, THubClient, TData>, new()
        where THub : SubscriberHubBase<THub, THubClient, THubSubscriberService, TData>
        where THubClient : class
    {
        private static THubSubscriberService __service;

        private static readonly object _serviceLocker = new();

        protected THubSubscriberService Service
        {
            get
            {
                if (__service == null)
                {
                    lock (_serviceLocker)
                    {
                        if (__service == null)
                        {
                            __service = new THubSubscriberService();
                            __service.AddServiceProvider(Context.GetHttpContext()!.RequestServices);
                        }
                    }
                }

                return __service;
            }
        }

        /// <summary> Unsubscribes from all subscribed updates. </summary>
        [SignalRHidden]
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Service.UnsubscribeFromAll(Context);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
