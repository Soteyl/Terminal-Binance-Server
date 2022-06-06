using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary>
    /// Result of <see cref="Handlers.GetTransactionsByOrderHandler"/>. Contains a collection of trades.
    /// </summary>
    public class GetTransactionsByOrderResult
    {
        /// <summary>
        /// Collection of binance spot market order trades
        /// </summary>
        public IEnumerable<BinanceTrade> Trades { get; set; }
    }
}
