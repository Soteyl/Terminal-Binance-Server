using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceFuturesUSDT : CryptoFutures
    {
        private IBinanceClientFuturesUsdt _client;
        
        public BinanceFuturesUSDT(IBinanceClient binanceClient, string mainCoin) 
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
