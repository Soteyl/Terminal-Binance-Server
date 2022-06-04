namespace Ixcent.CryptoTerminal.Api.Hubs
{
    using Broadcast;
    using Clients;

    public class BinanceSpotOpenOrdersHub: SubscriberHub<BinanceSpotOpenOrdersHub, IBinanceSpotOpenOrdersHubClient, BinanceSpotOpenOrdersService>
    {
    }
}
