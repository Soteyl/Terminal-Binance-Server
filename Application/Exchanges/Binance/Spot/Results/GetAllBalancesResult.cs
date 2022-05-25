using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary>
    /// Result for getting all Binance balances.
    /// </summary>
    public class GetAllBalancesResult
    {
        public IEnumerable<BinanceBalance> AvailableBalances { get; set; }
    }
}
