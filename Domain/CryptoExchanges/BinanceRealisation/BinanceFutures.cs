using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
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
    public class BinanceFutures : CryptoFutures
    {
<<<<<<< HEAD:Domain/CryptoExchanges/BinanceRealisation/BinanceFutures.cs
        private IBinanceClient _client;
        private IExchangeClient _exClient;
        public BinanceFutures(IBinanceClient binanceClient, IExchangeClient exClient, string mainCoin) 
=======
        private IBinanceClientFuturesUsdt _client;
        
<<<<<<< HEAD:Domain/CryptoExchanges/BinanceRealisation/BinanceFuturesUSDT.cs
        public BinanceFuturesUSDT(IBinanceClient binanceClient, string mainCoin) 
>>>>>>> 25e1686 (#210: Implemented GetUSDTBalance):Domain/CryptoExchanges/BinanceRealisation/BinanceFuturesUSDT.cs
=======
        public BinanceFutures(IBinanceClient binanceClient, string mainCoin) 
>>>>>>> 4408cb0 (#218  : Fixed naming):Domain/CryptoExchanges/BinanceRealisation/BinanceFutures.cs
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

        public override async Task<IEnumerable<FuturesOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceFuturesOrder>> ordersList = await _client.Order.GetOpenOrdersAsync();

            // TODO implement converters from binance to crypto terminal enums
            var selectedOrdersList = ordersList.Data.ToList().Select(
                order => new FuturesOrder(
                        order.Symbol,
                        order.Quantity,
                        (OrderSide)(int) order.Side,
                        (OrderType)(int) order.Type,
                        (PositionSide)(int)order.PositionSide,
                        order.CreatedTime,
                        order.Price,
                        (TimeInForce)(int) order.TimeInForce,
                        order.ReduceOnly
                    ));

            return selectedOrdersList;
        }

<<<<<<< HEAD:Domain/CryptoExchanges/BinanceRealisation/BinanceFuturesUSDT.cs
        public override async Task<IEnumerable<BinanceFuturesUsdtTrade>> GetOrdersHistory()
=======
        public override Task<IEnumerable<FuturesOrder>> GetOrdersHistory()
>>>>>>> 4408cb0 (#218  : Fixed naming):Domain/CryptoExchanges/BinanceRealisation/BinanceFutures.cs
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
            WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> balanceResult = await _client.Account.GetBalanceAsync();

            var coinBalanceUSDT = balanceResult.Data.ToList().Find(balance => balance.Asset == MainCoin);

            return new CoinBalance(MainCoin, coinBalanceUSDT?.AvailableBalance ?? 0, (coinBalanceUSDT?.WalletBalance - coinBalanceUSDT?.AvailableBalance) ?? 0);
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            // TODO implement position side converter
            WebCallResult<BinanceFuturesPlacedOrder> placeOrderResult = await _client.Order.PlaceOrderAsync(
                symbol: order.Symbol,
                side: order.OrderSide.ConvertToBinanceOrderSide(),
                type: order.OrderType.ConvertToBinanceOrderType(),
                quantity: order.Amount,
                reduceOnly: order.ReduceOnly,
                positionSide: (Binance.Net.Enums.PositionSide)(int)order.PositionSide
                );

            return new MakeOrderResult(placeOrderResult.Success, placeOrderResult.Error?.Message);
        }
        
    }
}
