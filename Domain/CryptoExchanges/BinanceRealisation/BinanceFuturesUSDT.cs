using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceFuturesUSDT : CryptoFutures
    {
<<<<<<< HEAD:Domain/CryptoExchanges/BinanceRealisation/BinanceFutures.cs
        private IBinanceClient _client;
        private IExchangeClient _exClient;
        public BinanceFutures(IBinanceClient binanceClient, IExchangeClient exClient, string mainCoin) 
=======
        private IBinanceClientFuturesUsdt _client;
        
        public BinanceFuturesUSDT(IBinanceClient binanceClient, string mainCoin) 
>>>>>>> 25e1686 (#210: Implemented GetUSDTBalance):Domain/CryptoExchanges/BinanceRealisation/BinanceFuturesUSDT.cs
            : base(mainCoin)
        {
            _exClient = exClient;
            _client = binanceClient;
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

        public override async Task<IEnumerable<BinanceFuturesUsdtTrade>> GetOrdersHistory()
        {

            WebCallResult<IEnumerable<BinancePrice>> callPricesResult = await _client.FuturesUsdt.Market.GetPricesAsync();

            IEnumerable<BinancePrice> pricesList = callPricesResult.Data;

            List<BinanceFuturesUsdtTrade> tradesList = new List<BinanceFuturesUsdtTrade>();

            foreach (var price in pricesList)
            {
                var closedOrdersResult = (await _client.FuturesUsdt.Order.GetUserTradesAsync(price.Symbol)).Data;

                if (closedOrdersResult != null)
                    tradesList.AddRange(closedOrdersResult);

            }


            return tradesList;
        }

        public override async Task<CoinBalance> GetUSDTBalance()
        {
            var task = await _client.Account.GetBalanceAsync();

            bool isSuccess = task.GetResultOrError(out var balanceResult, out Error? error);

            var coinBalanceUSDT = balanceResult?.ToList().Find(balance => balance.Asset == MainCoin);

            return new CoinBalance(MainCoin, coinBalanceUSDT?.AvailableBalance ?? 0, (coinBalanceUSDT?.WalletBalance - coinBalanceUSDT?.AvailableBalance) ?? 0);
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            throw new NotImplementedException();
        }
        
    }
}
