using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Shared;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceFutures : CryptoFutures
    {
        private IBinanceClientFuturesUsdt _client;
        private IExchangeClient _exClient;
        
        public BinanceFutures(IBinanceClientFuturesUsdt binanceFuturesClient, IExchangeClient exClient, string mainCoin) 
            : base(mainCoin)
        {
            _exClient = exClient;
            _client = binanceFuturesClient;

        }

        public override void AdjustLeverage(int value)
        {
            throw new NotImplementedException();
        }

        public override void ChangeMarginType()
        {
            throw new NotImplementedException();
        }

        public override async Task<OrderBook> GetDepthOfMarket(string firstQuote)
        {
            WebCallResult<ICommonOrderBook> resultOrderBook = await _exClient.GetOrderBookAsync(firstQuote + MainCoin);
            
            var preparedResult = new OrderBook(resultOrderBook.Data.CommonBids.Select(bid => new OrderBookEntry(bid.Quantity, bid.Price)),
                                                resultOrderBook.Data.CommonAsks.Select(ask => new OrderBookEntry(ask.Quantity, ask.Price)));

            return preparedResult;
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
