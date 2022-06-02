using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    public class OrdersHistoryResult
    {
        public IEnumerable<ICommonSymbol> Symbols { get; set; }
    }
}
