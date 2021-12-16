using System.Linq;

namespace CryptoTerminal.Models.CryptoExchanges
{
    public abstract class CryptoExchange
    {
        public abstract List<CryptoFutures> GetFutures();

        public abstract CryptoSpot GetCryptoSpot();
    }
}
