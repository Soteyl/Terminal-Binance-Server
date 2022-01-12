using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;

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

        public override async Task<IEnumerable<FuturesTrade>> GetTradesHistory()
        {
            WebCallResult<IEnumerable<BinancePrice>> callPricesResult = await _client.Market.GetPricesAsync();

            IEnumerable<BinancePrice> pricesList = callPricesResult.Data;

            List<FuturesTrade> tradesList = new List<FuturesTrade>();

            foreach (var price in pricesList)
            {
                var closedOrdersResult = (await _client.Order.GetUserTradesAsync(price.Symbol)).Data
                    .Select(
                        trade => new FuturesTrade(
                            symbol: trade.Symbol,
                            price: trade.Price,
                            quantity: trade.Quantity,
                            realizedPnl: trade.RealizedPnl,
                            commission: trade.Commission,
                            orderSide: trade.Side,
                            positionSide: trade.PositionSide,
                            tradeTime: trade.TradeTime,
                            id: trade.Id,
                            orderId: trade.OrderId,
                            buyer: trade.Buyer
                    ));

                if (closedOrdersResult != null)
                    tradesList.AddRange(closedOrdersResult);

            }

            return tradesList;
        }

        public override async Task<IEnumerable<FuturesOrder>> GetOrdersHistory()
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
                positionSide: order.PositionSide
                );

            return new MakeOrderResult(placeOrderCaller.Success, placeOrderCaller.Error?.Message);
        }
        
    }
}
