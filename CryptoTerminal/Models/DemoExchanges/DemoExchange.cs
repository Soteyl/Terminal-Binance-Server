using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoExchange : CryptoExchanges.CryptoExchange
    {
        private BinanceClient _client;

        private DemoSpot _spot;

        public DemoExchange(IAccessDemoStorage demoStorage)
        {
            _client = new BinanceClient();
            _spot = new DemoSpot(demoStorage, _client.Spot, "user-key");
        }
        public override CryptoSpot GetCryptoSpot()
        {
            return _spot;
        }

        public override List<CryptoFutures> GetFutures()
        {
            throw new NotImplementedException();
        }
    }
}
