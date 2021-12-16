using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models
{
    public class DemoExchange : CryptoExchanges.CryptoExchange
    {
        public override CryptoSpot GetCryptoSpot()
        {
            throw new NotImplementedException();
        }

        public override List<CryptoFutures> GetFutures()
        {
            throw new NotImplementedException();
        }
    }
}
