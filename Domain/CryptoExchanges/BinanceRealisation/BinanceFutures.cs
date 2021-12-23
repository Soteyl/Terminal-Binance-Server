using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceFutures : CryptoFutures
    {
        private IBinanceClientFuturesUsdt _client;
        
        public BinanceFutures(IBinanceClient binanceClient, string mainCoin) 
            : base(mainCoin)
        {
            _client = binanceClient.FuturesUsdt;
        }

        public override void AdjustLeverage(int value)
        {
            throw new NotImplementedException();
        }

        public override void ChangeMarginType()
        {
            throw new NotImplementedException();
        }

        public override Task<OrderBook> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override Task<FuturesOrder> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public override Task<FuturesOrder> GetOrdersHistory()
        {
            throw new NotImplementedException();
        }

        public override Task<CoinBalance> GetUSDTBalance()
        {
            throw new NotImplementedException();
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            throw new NotImplementedException();
        }
        
    }
}
