using Microsoft.AspNetCore.SignalR;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    /// <summary>
    /// Service for Subscriber hub which allows to shedule responses
    /// </summary>
    /// <typeparam name="THub">Subscriber hub</typeparam>
    /// <typeparam name="THubClient">Subscriber hub client</typeparam>
    /// <typeparam name="TData">Additional data on subscribe</typeparam>
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
