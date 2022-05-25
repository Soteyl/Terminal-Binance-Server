using Binance.Net.Objects.Spot.MarketData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary> Result of getting all prices. </summary>
    public class SymbolPricesResult
    {
        public IEnumerable<BinancePrice> Prices { get; set; }
    }
}
