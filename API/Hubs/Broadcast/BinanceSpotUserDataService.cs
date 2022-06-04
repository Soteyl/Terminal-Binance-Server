using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Binance.Net.Objects.Spot.UserStream;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    using Application.Exchanges.Binance.Spot.Realtime;
    using Clients;
    using EFData;

    public class BinanceSpotUserDataService : ISubscriberHubService<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient, object?>
    {
        private readonly UserData _userData = UserData.GetInstance();

        private IHubContext<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient> _hubContext;

        private CryptoTerminalContext _database;

        public void AddServiceProvider(IServiceProvider provider)
        {
            _database = provider.GetService<CryptoTerminalContext>()!;
            _hubContext = provider.GetService<IHubContext<BinanceSpotUserDataHub, IBinanceSpotUserDataHubClient>>()!;
            _userData.OnOrderUpdate += _userData_OnOrderUpdate;
            _userData.OnAccountPositionUpdate += _userData_OnAccountPositionUpdate;
            _userData.OnAccountBalanceUpdate += _userData_OnAccountBalanceUpdate;
            _userData.OnOcoOrderUpdate += _userData_OnOcoOrderUpdate;
        }

        public async Task Subscribe(object? data, string groupName, HubCallerContext context)
        {
            var token = await _database.ExchangeTokens.FirstOrDefaultAsync(token => token.UserId.Equals(context.UserIdentifier) && token.Exchange.Equals("Binance"));
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

        private void _userData_OnOcoOrderUpdate(object? sender, BinanceStreamOrderList e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveOcoOrderUpdate(e);
        }

        private void _userData_OnAccountBalanceUpdate(object? sender, BinanceStreamBalanceUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveStreamBalanceUpdate(e);
        }

        private void _userData_OnAccountPositionUpdate(object? sender, BinanceStreamPositionsUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveAccountPositionUpdate(e);
        }

        private void _userData_OnOrderUpdate(object? sender, BinanceStreamOrderUpdate e)
        {
            _hubContext.Clients.Client(sender!.ToString()!).ReceiveOpenOrderUpdate(e);
        }
    }
}
