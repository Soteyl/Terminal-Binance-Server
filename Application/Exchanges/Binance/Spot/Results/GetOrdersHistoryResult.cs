using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.ExchangeInterfaces;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{ 
    /// <summary>
    /// Result that is returned by main method of <see cref="GetOrdersHistoryHandler"/>.
    /// </summary>
    public class OrdersHistoryResult
    {
        /// <summary>
        /// List of ever done trades.
        /// </summary>
        public IEnumerable<BinanceTrade> ClosedOrders { get; set; }
    }
}
