﻿using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    public class BinanceCryptoExchange : ICryptoExchange
    {
        private BinanceClient _client;

        private BinanceSpot _spot;

        private List<CryptoFutures> _futures;

        private string? _token;

        private string? _secret;

        public BinanceCryptoExchange(string apiToken, string apiSecret): this()
        {
            _token = apiToken;
            _secret = apiSecret;
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

            _futures = new List<CryptoFutures>();
            _futures.Add(new BinanceFuturesUSDT(_client.FuturesUsdt, _client));
        }

        public ICryptoSpot GetCryptoSpot()
        {
            return _spot;
        }

        public IEnumerable<CryptoFutures> GetFutures()
        {
            return _futures;
        }

        public async Task<IEnumerable<ICommonSymbol>> GetSymbols()
        {
            return (await ((IExchangeClient)_client).GetSymbolsAsync()).Data;
        }
    }
}
