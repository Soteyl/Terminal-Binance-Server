using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary> Result of getting open orders cotains collection with all open orders </summary>
    public class OpenOrdersResult
    {
        public IEnumerable<BinanceOrder> Orders { get; set; }
    }
}
