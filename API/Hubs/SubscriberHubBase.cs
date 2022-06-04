using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    [SignalRHidden]
    [SignalRHub()]
    public abstract class SubscriberHubBase<THub, THubClient, THubSubscriberService, TData> : Hub<THubClient>
        where THubSubscriberService : SubscriberHubService<THub, THubClient, TData>, new()
        where THub : SubscriberHubBase<THub, THubClient, THubSubscriberService, TData>
        where THubClient : class
    {
        private static THubSubscriberService __service;

        protected THubSubscriberService Service
        {
            get
            {
                if (__service == null)
                {
                    __service = new THubSubscriberService();
                    __service.AddServiceProvider(Context.GetHttpContext()!.RequestServices);
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
