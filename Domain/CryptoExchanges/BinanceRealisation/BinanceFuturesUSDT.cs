using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    using Data;
    using Ixcent.CryptoTerminal.Domain.Database;
    using Results;

    public class BinanceFuturesUSDT : CryptoFutures
    {
        private IBinanceClientFuturesUsdt _client;
        private IExchangeClient _exClient;
        private BinacneExchange
        private TwapOrderRecord _nextTwapToExecute;
        private List<TwapOrderRecord> _allTwapRecords = new List<TwapOrderRecord>();
        private Thread _twapThread;

        public BinanceFuturesUSDT(IBinanceClientFuturesUsdt binanceFuturesClient, IExchangeClient exClient, BinanceExchangeContext binanceDbContext) 
            : base("USDT")
        {
            _exClient = exClient;
            _client = binanceFuturesClient;

            _twapThread = new Thread(TwapThread);
            _twapThread.Start();
        }

        public override async Task<BinanceFuturesInitialLeverageChangeResult> AdjustLeverage(string symbol, int leverageValue)
        {
            WebCallResult<BinanceFuturesInitialLeverageChangeResult> adjustLeverageCaller =  await _client.ChangeInitialLeverageAsync(symbol, leverageValue);

            return adjustLeverageCaller.Data;
        }

        public override async Task<BinanceFuturesChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType)
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

            var selectedOrdersList = ordersList.Data.ToList().Select(
                order => new FuturesOrder(
                        symbol: order.Symbol,
                        amount: order.Quantity,
                        orderSide: order.Side,
                        orderType: order.Type,
                        positionSide: order.PositionSide,
                        date: order.CreatedTime,
                        id: order.OrderId,
                        price: order.Price,
                        tif: order.TimeInForce,
                        reduceOnly: order.ReduceOnly
                    ));

            return selectedOrdersList;
        }

        public override void CancelOrder(FuturesOrder order)
        {
            _client.Order.CancelOrderAsync(order.Symbol, order.Id);
        }

        public override async Task<IEnumerable<BinanceFuturesUsdtTrade>> GetTradesHistory()
        {
            WebCallResult<IEnumerable<BinancePrice>> callPricesResult = await _client.Market.GetPricesAsync();

            IEnumerable<BinancePrice> pricesList = callPricesResult.Data;

            List<BinanceFuturesUsdtTrade> tradesList = new List<BinanceFuturesUsdtTrade>();

            foreach (var price in pricesList)
            {
                var closedOrdersResult = (await _client.Order.GetUserTradesAsync(price.Symbol)).Data;

                if (closedOrdersResult != null)
                    tradesList.AddRange(closedOrdersResult);

            }

            return tradesList;
        }

        public override async Task<IEnumerable<BinanceFuturesUsdtTrade>> GetOrdersHistory()
        {
            throw new NotImplementedException();
        }

        public override async Task<CoinBalance> GetBalance()
        {
            WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> balanceCaller = await _client.Account.GetBalanceAsync();

            var coinBalance = balanceCaller.Data.ToList().Find(balance => balance.Asset == MainCoin);

            return new CoinBalance(MainCoin, coinBalance?.AvailableBalance ?? 0, (coinBalance?.WalletBalance - coinBalance?.AvailableBalance) ?? 0);
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            WebCallResult<BinanceFuturesPlacedOrder> placeOrderCaller = await _client.Order.PlaceOrderAsync(
                symbol: order.Symbol,
                side: order.OrderSide,
                type: order.OrderType,
                quantity: order.Amount,
                reduceOnly: order.ReduceOnly,
                positionSide: order.PositionSide,
                timeInForce: order.TIF,
                price: order.Price
                );

            return new MakeOrderResult(placeOrderCaller.Success, placeOrderCaller.Error?.Message);
        }

        public override async Task<MakeOrderResult> MakeTWAPOrder(TwapOrder twapOrder)
        {
            if ((await GetBalance()).Free < twapOrder.Quantity)
                return new MakeOrderResult(false, "Not enough funds!");

            List<TwapOrderRecord> records = new List<TwapOrderRecord>();
            DateTime startTime = twapOrder.StartTime;
            int steps = (int)twapOrder.Quantity / (int)twapOrder.StepSize;
            TimeSpan stepTime = twapOrder.Duration / steps;

            while (twapOrder.Quantity >= 0)
            {
                decimal quantity = twapOrder.Quantity > twapOrder.StepSize ? twapOrder.StepSize : twapOrder.Quantity;
                records.Add(new TwapOrderRecord(
                    symbol: twapOrder.Symbol,
                    quantity: quantity,
                    executeTime: startTime,
                    positionSide: twapOrder.Position,
                    orderSide: twapOrder.Side
                ));
                startTime = startTime.Add(stepTime);
                twapOrder.Quantity -= twapOrder.StepSize;
            }

            foreach (var record in records)
            {
                // TODO replace with insert into db context
                _allTwapRecords.Add(record);
                if (record.ExecuteTime < _nextTwapToExecute.ExecuteTime)
                    _nextTwapToExecute = record;
            }
            return new MakeOrderResult(true, "Placed order successfully!");
        }
        
        public void CancelTwapOrder(TwapOrderRecord record)
        {
            // TODO replace with twap orders db context
            _allTwapRecords.Remove(_nextTwapToExecute);
            _nextTwapToExecute = _allTwapRecords.OrderBy(order => order.ExecuteTime).First();
        }

        // TODO separate in different class
        private async void TwapThread()
        {
            while (_twapThread.IsAlive)
            {
                if (DateTime.Now >= _nextTwapToExecute.ExecuteTime)
                {
                    MakeOrderResult placeOrderResult = await MakeOrder(new FuturesOrder(
                        symbol: _nextTwapToExecute.Symbol,
                        amount: _nextTwapToExecute.Quantity,
                        orderSide: _nextTwapToExecute.OrderSide,
                        orderType: Enums.OrderType.Market,
                        positionSide: _nextTwapToExecute.PositionSide,
                        date: _nextTwapToExecute.ExecuteTime
                    ));

                    // TODO replace with twap orders db context
                    if (placeOrderResult.Success)
                    {
                        // TODO warn user about unsuccessful order
                    }
                    _allTwapRecords.Remove(_nextTwapToExecute);
                    _nextTwapToExecute = _allTwapRecords.OrderBy(order => order.ExecuteTime).First();
                }
                Thread.Sleep(1);
            }
        }

    }
}
