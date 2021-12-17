using System.Security;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : CryptoExchange
    {
        private BinanceClient _client;

        private BinanceSpot _spot;

        private string _token;

        private string _secret;

        public BinanceCryptoExchange(string apiToken, string apiSecret)
        {
            _token = apiToken;
            _secret = apiSecret;
            _client = new BinanceClient(
                new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apiToken, apiSecret)
                });

            _spot = new BinanceSpot(_client.Spot);
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
