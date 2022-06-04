using Microsoft.AspNetCore.SignalR;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    public interface ISubscriberHubService<THub, THubClient, TData>
        where THub : Hub<THubClient>
        where THubClient : class
    {
        void AddServiceProvider(IServiceProvider serviceProvider);

        Task Subscribe(TData data, string groupName, HubCallerContext context);

        Task Unsubscribe(string groupName, HubCallerContext context);

        Task UnsubscribeFromAll(HubCallerContext context);
    }
}
