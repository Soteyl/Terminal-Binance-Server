using Binance.Net;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using CryptoExchange.Net.ExchangeInterfaces;
    using CryptoExchanges;
    using System.Threading.Tasks;

    public class DemoExchange : ICryptoExchange
    {
        private BinanceClient _client;

        private DemoSpot _spot;

        public DemoExchange(IAccessDemoStorage demoStorage, string userKey)
        {
            _client = new BinanceClient();
            _spot = new DemoSpot(demoStorage, _client, _client.Spot, userKey);
        }
        public ICryptoSpot GetCryptoSpot()
        {
            return _spot;
        }

        public IEnumerable<CryptoFutures> GetFutures()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ICommonSymbol>> GetSymbols()
        {
            throw new NotImplementedException();
        }
    }
}
