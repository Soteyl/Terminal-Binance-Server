﻿using System.Security;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : ICryptoExchange
    {
        private BinanceClient _client;

        private BinanceSpot _spot;

        private List<CryptoFutures> _futures;

        private string? _token;

        private string? _secret;

        public BinanceCryptoExchange(string apiToken, string apiSecret, IBinanceFuturesExchangeContext context): this(context)
        {
            _token = apiToken;
            _secret = apiSecret;
            _client = new BinanceClient(
                new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apiToken, apiSecret)
                });
        }

        public BinanceCryptoExchange(IBinanceFuturesExchangeContext context)
        {
            _client = new BinanceClient();
            _spot = new BinanceSpot(_client.Spot, _client.General, _client);

            _futures = new List<CryptoFutures>();
            _futures.Add(new BinanceFuturesUSDT(_client.FuturesUsdt, _client, context));
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
