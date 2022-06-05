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

        /// <summary>
        /// Receives binance spot account position update
        /// </summary>
        /// <param name="positionUpdate">account position update</param>
        Task ReceiveAccountPositionUpdate(BinanceStreamPositionsUpdate positionUpdate);

        /// <summary>
        /// Receives binance spot account balance update
        /// </summary>
        /// <param name="balanceUpdate">Balance update</param>
        Task ReceiveStreamBalanceUpdate(BinanceStreamBalanceUpdate balanceUpdate);

        /// <summary>
        /// Reveives binance spot OCO order updates
        /// </summary>
        /// <param name="orderList">List of orders for Oco order update</param>
        Task ReceiveOcoOrderUpdate(BinanceStreamOrderList orderList);
    }
}
