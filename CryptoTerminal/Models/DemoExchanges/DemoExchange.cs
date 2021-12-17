using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;

namespace CryptoTerminal.Models
{
    public class DemoExchange : CryptoExchanges.CryptoExchange
    {
        private BinanceClient _client;

        private DemoSpot _spot;

        public DemoExchange()
        {
            _client = new BinanceClient();
            _spot = new DemoSpot(_client.Spot);
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
