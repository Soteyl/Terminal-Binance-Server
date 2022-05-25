using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Models
{
    public class GetAllBalancesSpotResult
    {
        public IEnumerable<BinanceBalance> AvailableBalances { get; set; }
    }
}
