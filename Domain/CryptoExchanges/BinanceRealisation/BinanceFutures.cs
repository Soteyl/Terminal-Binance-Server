using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
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
        private IExchangeClient _exClient;
        
        public BinanceFutures(IBinanceClient binanceClient, IExchangeClient exClient, string mainCoin) 
            : base(mainCoin)
        {
            _exClient = exClient;
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

        public override async Task<IEnumerable<FuturesOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceFuturesOrder>> ordersList = await _client.Order.GetOpenOrdersAsync();

            var selectedOrdersList = ordersList.Data.ToList().Select(
                order => new FuturesOrder(
                        order.Symbol,
                        order.Quantity,
                        (OrderSide)(int) order.Side,
                        (OrderType)(int) order.Type,
                        order.CreatedTime,
                        order.Price,
                        (TimeInForce)(int) order.TimeInForce,
                        order.ReduceOnly
                    ));

            return selectedOrdersList;
        }

        public override Task<IEnumerable<FuturesOrder>> GetOrdersHistory()
        {
            throw new NotImplementedException();
        }

        public override async Task<CoinBalance> GetUSDTBalance()
        {
            WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> balanceResult = await _client.Account.GetBalanceAsync();

            var coinBalanceUSDT = balanceResult.Data.ToList().Find(balance => balance.Asset == MainCoin);

            return new CoinBalance(MainCoin, coinBalanceUSDT?.AvailableBalance ?? 0, (coinBalanceUSDT?.WalletBalance - coinBalanceUSDT?.AvailableBalance) ?? 0);
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            throw new NotImplementedException();
        }
        
    }
}
