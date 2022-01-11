using CryptoExchange.Net.ExchangeInterfaces;
using System.Linq;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public interface ICryptoExchange
    {
        IEnumerable<CryptoFutures> GetFutures();

        ICryptoSpot GetCryptoSpot();

        Task<IEnumerable<ICommonSymbol>> GetSymbols();
    }
}
