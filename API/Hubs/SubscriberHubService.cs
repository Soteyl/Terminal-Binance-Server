﻿using Microsoft.AspNetCore.SignalR;

namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Ixcent.CryptoTerminal.Infrastructure;

    /// <summary>
    /// Abstract class of subscribe hub singleton service used in inheritences of <see cref="SubscriberHubBase{THub, THubClient, THubSubscriberService}"/>
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    /// <typeparam name="THubClient"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public abstract class SubscriberHubService<THub, THubClient, TData> : IDisposable
        where THub : Hub<THubClient>
        where THubClient : class
    {
        protected IHubContext<THub, THubClient> _hubContext;

        protected readonly ConnectionMapping<string> _subscribers = new ConnectionMapping<string>();

        public SubscriberHubService()
        {
            _subscribers.OnKeyEmpty += SubscriberKeyEmpty;
        }

        public virtual SubscriberHubService<THub, THubClient, TData> AddHubContext(IHubContext<THub, THubClient> hubContext)
        {
            if (_hubContext == null)
                _hubContext = hubContext;

            return this;
        }

        /// <summary>
        /// Subscribes caller to group
        /// </summary>
        /// <param name="data">Additional data</param>
        /// <param name="groupName">Name of group</param>
        /// <param name="context">Caller context</param>
        public virtual async Task Subscribe(TData data, string groupName, HubCallerContext context)
        {
            if (_hubContext == null) return;

            groupName = groupName.ToUpper();
            await _hubContext.Groups.AddToGroupAsync(context.ConnectionId, groupName);
            _subscribers.Add(groupName, context.ConnectionId);
        }

        /// <summary>
        /// Unubscribes caller from group
        /// </summary>
        /// <param name="groupName">Name of group</param>
        /// <param name="context">Caller context</param>
        public virtual async Task Unsubscribe(string groupName, HubCallerContext context)
        {
            if (_hubContext == null) return;

            groupName = groupName.ToUpper();
            await _hubContext.Groups.RemoveFromGroupAsync(context.ConnectionId, groupName);
            _subscribers.Remove(groupName, context.ConnectionId);
        }

        /// <summary> Unsubscribes caller from all groups (only in private list, not in HubContext)</summary>
        /// <param name="context">Caller context</param>
        public virtual Task UnsubscribeFromAll(HubCallerContext context)
        {
            _subscribers.RemoveByConnectionId(context.ConnectionId);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Unsubscribes from internal events
        /// </summary>
        public virtual void Dispose()
        {
            _subscribers.OnKeyEmpty -= SubscriberKeyEmpty;
        }

        protected virtual void SubscriberKeyEmpty(string key)
        { }
    }
}
