using System.Linq;

namespace CryptoTerminal.Models.CryptoExchanges
{
    public interface ICryptoExchange
    {
        List<CryptoFutures> GetFutures();

        CryptoSpot GetCryptoSpot();
    }
}
