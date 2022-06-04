using Binance.Net.Objects.Spot.UserStream;

namespace Ixcent.CryptoTerminal.Api.Hubs.Clients
{
    /// <summary>
    /// Client for <see cref="BinanceSpotUserDataHub"/>
    /// </summary>
    public interface IBinanceSpotUserDataHubClient
    {
        /// <summary>
        /// Reveives binance spot open orders update
        /// </summary>
        /// <param name="orderUpdate">order updates</param>
        Task ReceiveOpenOrderUpdate(BinanceStreamOrderUpdate orderUpdate);

        Task ReceiveAccountPositionUpdate(BinanceStreamPositionsUpdate positionUpdate);

        Task ReceiveStreamBalanceUpdate(BinanceStreamBalanceUpdate balanceUpdate);

        Task ReceiveOcoOrderUpdate(BinanceStreamOrderList orderList);
    }
}
