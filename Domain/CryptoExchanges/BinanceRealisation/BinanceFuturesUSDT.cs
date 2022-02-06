using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    using CryptoExchanges;
    using Data;
    using Results;

    public class BinanceFuturesUSDT : CryptoFutures
    {
        private IBinanceClientFuturesUsdt _client;

        private IExchangeClient _exClient;
        
        public BinanceFuturesUSDT(IBinanceClientFuturesUsdt binanceFuturesClient, IExchangeClient exClient) 
            : base("USDT")
        {
            _exClient = exClient;
            _client = binanceFuturesClient;
        }

        public override async Task<AdjustLeverageResult> AdjustLeverage(string symbol, int leverageValue)
        {
            WebCallResult<BinanceFuturesInitialLeverageChangeResult> adjustLeverageCaller =  await _client.ChangeInitialLeverageAsync(symbol, leverageValue);

            return adjustLeverageCaller.Data;
        }

        public override async Task<ChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType)
        {
            WebCallResult<BinanceFuturesChangeMarginTypeResult> changeMarginCaller = await _client.ChangeMarginTypeAsync(symbol, marginType);

            return changeMarginCaller.Data;
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

            return ordersList.Data.Cast<FuturesOrder>();
        }

        public override void CancelOrder(FuturesOrder order)
        {
            _client.Order.CancelOrderAsync(order.Symbol, order.Id);
        }

        public override async Task<IEnumerable<FuturesTrade>> GetTradesHistory()
        {
            WebCallResult<IEnumerable<BinancePrice>> callPricesResult = await _client.Market.GetPricesAsync();

            IEnumerable<BinancePrice> pricesList = callPricesResult.Data;

            List<FuturesTrade> tradesList = new List<FuturesTrade>();

            foreach (var price in pricesList)
            {
                var closedOrdersResult = (await _client.Order.GetUserTradesAsync(price.Symbol)).Data.Cast<FuturesTrade>();

                if (closedOrdersResult != null)
                    tradesList.AddRange(closedOrdersResult);

            }

            return tradesList;
        }

        public override async Task<IEnumerable<FuturesOrder>> GetOrdersHistory()
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<FuturesBalance>> GetBalances()
        {
            WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> balanceCaller = await _client.Account.GetBalanceAsync();

            return balanceCaller.Data.Cast<FuturesBalance>();
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            WebCallResult<BinanceFuturesPlacedOrder> placeOrderCaller = await _client.Order.PlaceOrderAsync(
                symbol: order.Symbol,
                side: order.OrderSide,
                type: order.OrderType,
                quantity: order.Amount,
                reduceOnly: order.ReduceOnly,
                positionSide: order.PositionSide
                );

            return new MakeOrderResult(placeOrderCaller.Success, placeOrderCaller.Error?.Message);
        }

        public override async Task<MakeOrderResult> ClosePosition(FuturesPosition position)
        {
            return await MakeOrder(new FuturesOrder
            {
                Symbol = position.Symbol,
                Amount = position.Quantity,
                OrderSide = OrderSide.Buy,
                OrderType = Enums.OrderType.Market,
                PositionSide = position.Side,
                Price = position.EntryPrice,
                Tif = TimeInForce.GoodTillExpiredOrCanceled,
                ReduceOnly = true,
                ClosePosition = true
            });
        }

        public override async Task<IEnumerable<FuturesPosition>> GetAllPositions()
        {
            WebCallResult<BinanceFuturesAccountInfo> accountInfoCaller = await _client.Account.GetAccountInfoAsync();

            IEnumerable<BinanceFuturesMarkPrice> markPrices = (await _client.Market.GetMarkPricesAsync()).Data;

            return accountInfoCaller.Data.Positions.Cast<FuturesPosition>();
        }
    }
}
