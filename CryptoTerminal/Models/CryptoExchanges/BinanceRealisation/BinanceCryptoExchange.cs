using System.Security;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : ICryptoExchange
    {
        private BinanceClient _client;

        private BinanceSpot _spot;

        private List<CryptoFutures> _futures;

        private string? _token;

        private string? _secret;

        public BinanceCryptoExchange(string apiToken, string apiSecret)
        {
            _token = apiToken;
            _secret = apiSecret;
            _client = new BinanceClient(
                new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apiToken, apiSecret)
                });
            _spot = new BinanceSpot(_client.Spot, _client.General, _client);
            
            _futures = new List<CryptoFutures>();
            _futures.Add(new BinanceFutures(_client, _client, "USDT"));
        }

        public BinanceCryptoExchange()
        {
            _client = new BinanceClient();
            _spot = new BinanceSpot(_client.Spot, _client.General, _client);
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
