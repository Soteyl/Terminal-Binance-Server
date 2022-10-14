using Binance.Net.Objects.Spot.UserStream;

using Ixcent.CryptoTerminal.Api.Hubs.Clients;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.StorageHandle;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    /// <summary>
    /// Hub Service for receiving user data from Binance spot account.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="ISubscriberHubService{THub, THubClient, TData}"/> <br/>
    /// <c>THub</c> is <see cref="BinanceSpotUserDataHub"/><br/>
    /// <c>THubClient</c> is <see cref="IBinanceSpotUserDataHubClient"/><br/>
    /// <c>TData</c> is <see cref="object"/>?
    /// </remarks>
    public class BinanceSpotUserDataService : ISubscriberHubService<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient, object?>
    {
        private readonly UserData _userData = UserData.GetInstance();

        private IHubContext<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient> _hubContext;

        private CryptoTerminalContext _database;

        public void AddServiceProvider(IServiceProvider provider)
        {
            _database = provider.GetService<CryptoTerminalContext>()!;
            _hubContext = provider.GetService<IHubContext<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient>>()!;
            _userData.OnOrderUpdate += OrderUpdate;
            _userData.OnAccountPositionUpdate += AccountPositionUpdate;
            _userData.OnAccountBalanceUpdate += AccountBalanceUpdate;
            _userData.OnOcoOrderUpdate += OcoOrderUpdate;
        }

        public async Task Subscribe(object? data, string groupName, HubCallerContext context)
        {
            ExchangeTokenEntity? token = await _database.ExchangeTokens.FirstOrDefaultAsync(token => token.UserId.Equals(context.UserIdentifier) && token.Exchange.Equals("Binance"));
            await _userData.Subscribe(new SpotOpenOrdersSubscriber
            {
                ConnectionId = context.ConnectionId,
                Token = token
            });
        }

        public async Task Unsubscribe(string groupName, HubCallerContext context)
        {
            await _userData.Unsubscribe(context.ConnectionId);
        }

        public async Task UnsubscribeFromAll(HubCallerContext context)
        {
            await _userData.Unsubscribe(context.ConnectionId);
        }

        private void OcoOrderUpdate(object? sender, BinanceStreamOrderList e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveOcoOrderUpdate(e);
        }

        private void AccountBalanceUpdate(object? sender, BinanceStreamBalanceUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveStreamBalanceUpdate(e);
        }

        private void AccountPositionUpdate(object? sender, BinanceStreamPositionsUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveAccountPositionUpdate(e);
        }

        private void OrderUpdate(object? sender, BinanceStreamOrderUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveOpenOrderUpdate(e);
        }
    }
}
