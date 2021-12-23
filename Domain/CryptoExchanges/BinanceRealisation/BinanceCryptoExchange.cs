﻿using System.Security;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : ICryptoExchange
    {
        private BinanceClient _client;

        private BinanceSpot _spot;

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
            throw new NotImplementedException();
        }
    }
}