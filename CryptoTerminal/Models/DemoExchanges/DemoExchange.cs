using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoExchange : CryptoExchanges.CryptoExchange
    {
        private BinanceClient _client;

        private DemoSpot _spot;

        public DemoExchange(IAccessDemoStorage demoStorage, string userKey)
        {
            _client = new BinanceClient();
            _spot = new DemoSpot(demoStorage, _client, _client.Spot, userKey);
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
