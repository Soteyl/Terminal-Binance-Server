using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Api.Hubs.Clients
{
    /// <summary>
    /// Client for <see cref="BinanceSpotOpenOrdersHub"/>
    /// </summary>
    public interface IBinanceSpotOpenOrdersHubClient
    {
        /// <summary>
        /// Reveives binance spot open orders update
        /// </summary>
        /// <param name="orders">collection with all open orders</param>
        void ReceiveOpenOrders(IEnumerable<BinanceOrder> orders);
    }
}
