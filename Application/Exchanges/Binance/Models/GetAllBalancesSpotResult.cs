using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Models
{
    /// <summary>
    /// Result for getting all Binance balances.
    /// </summary>
    public class GetAllBalancesSpotResult
    {
        public IEnumerable<BinanceBalance> AvailableBalances { get; set; }
    }
}
