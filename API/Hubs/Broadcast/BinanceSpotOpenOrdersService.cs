using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace Ixcent.CryptoTerminal.Api.Hubs.Broadcast
{
    using Application.Exchanges.Binance.Spot.Realtime;
    using Clients;
    using EFData;

    public class BinanceSpotOpenOrdersService : SubscriberHubService<BinanceSpotOpenOrdersHub, IBinanceSpotOpenOrdersHubClient> // TODO
    {
        private readonly UserData _userData = UserData.GetInstance();

        private CryptoTerminalContext _database;

        public override void AddServiceProvider(IServiceProvider provider)
        {
            base.AddServiceProvider(provider);
            _database = provider.GetService<CryptoTerminalContext>()!;
        }

        public override async Task Subscribe(object? data, string groupName, HubCallerContext context)
        {
            await _userData.Subscribe(new SpotOpenOrdersSubscriber
            {
                ConnectionId = context.ConnectionId,
                Token = await _database.ExchangeTokens.FirstAsync(token => token.UserId.Equals(context.UserIdentifier))
            });
            await base.Subscribe(data, groupName, context);
        }
    }
}
