using Binance.Net;
using Binance.Net.Objects;

using CryptoExchange.Net.Authentication;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : ICryptoExchange
    {
        private readonly BinanceClient _client;

        private readonly BinanceSpot _spot;

        private readonly List<CryptoFutures> _futures;
        public BinanceCryptoExchange(string apiToken, string apiSecret) : this()
        {
            _client = new BinanceClient(
                new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apiToken, apiSecret)
                });
        }

        public BinanceCryptoExchange()
        {
            _client = new BinanceClient();
            _spot = new BinanceSpot(_client.Spot, _client.General, _client);

            _futures = new List<CryptoFutures>
            {
                new BinanceFuturesUSDT(_client.FuturesUsdt, _client)
            };
        }

        public CryptoSpot GetCryptoSpot()
        {
            return _spot;
        }

        public List<CryptoFutures> GetFutures()
        {
            return _futures;
        }
    }
}
