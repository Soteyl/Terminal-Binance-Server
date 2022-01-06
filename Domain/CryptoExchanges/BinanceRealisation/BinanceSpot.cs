using Binance.Net;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    using Data;
    using Results;
    /// <summary>
    /// Implementation of Binance Spot instrument. <para/>
    /// 
    /// Inherited from <see cref="CryptoSpot"/>
    /// </summary>
    public class BinanceSpot : CryptoSpot
    {
        private IBinanceClientSpot _spot;

        private IExchangeClient _exchangeClient;

		private IBinanceClientGeneral _general;

        internal BinanceSpot(IBinanceClientSpot spot, IBinanceClientGeneral general, IExchangeClient exClient)
        {
            _spot = spot;
            _exchangeClient = exClient;
			_general = general;
        }

        public override void CancelOrder(SpotOrder order)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<CoinBalance>> GetCoinBalances()
        {
            WebCallResult<BinanceAccountInfo> info = await _general.GetAccountInfoAsync();

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return balances.Select(balance => new CoinBalance(balance.Asset, balance.Free, balance.Locked));
        }

        public override async Task<IEnumerable<BookPrice>> GetCoinPairs()
        {
            WebCallResult<IEnumerable<BinanceBookPrice>> resultBookPrices = await _spot.Market.GetAllBookPricesAsync();
            
            return resultBookPrices.Data.Select(a => a.ToIxcentBookPrice());
        }

        public override async Task<OrderBook> GetDepthOfMarket(string symbol)
        {
            WebCallResult<ICommonOrderBook> resultOrderBook = await _exchangeClient.GetOrderBookAsync(symbol);
            // Convert ISymbolOrderBookEntry to OrderBookEntry
            var preparedResult = new OrderBook(resultOrderBook.Data.CommonBids.Select(bid => new OrderBookEntry(bid.Quantity, bid.Price)), 
                                                resultOrderBook.Data.CommonAsks.Select(bid => new OrderBookEntry(bid.Quantity, bid.Price)));
            return preparedResult;
        }

        public override async Task<IEnumerable<SpotOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceOrder>> resultBinanceOrders = await _spot.Order.GetOpenOrdersAsync();

            return resultBinanceOrders.Data.Select(a => new SpotOrder(a.Symbol, a.Quantity, a.Side, a.Type, price: a.Price));
        }

        public override async Task<IEnumerable<ICommonOrder>> GetOrderHistory()
        {
            var orderHistory = new List<ICommonOrder>();
            IEnumerable<ICommonSymbol> symbols = (await _exchangeClient.GetSymbolsAsync()).Data;
            foreach (ICommonSymbol symbol in symbols)
            {
                orderHistory.AddRange((await _exchangeClient.GetClosedOrdersAsync(symbol.CommonName)).Data);
            }
            return orderHistory;
        }

        public override async Task<IEnumerable<ICommonTrade>> GetTransactionsHistory()
        {
            var transactionHistory = new List<ICommonTrade>();
            IEnumerable<ICommonOrder> orders = await GetOrderHistory();
            foreach (ICommonOrder order in orders)
            {
                transactionHistory.AddRange((await _exchangeClient.GetTradesAsync(order.CommonId, order.CommonSymbol)).Data);
            }
            return transactionHistory;
        }

        public override async Task<MakeOrderResult> MakeOrder(SpotOrder order)
        {
            WebCallResult<BinancePlacedOrder> resultPlaceOrder = 
              await _spot.Order.PlaceOrderAsync(order.Pair,
                                                order.OrderSide,
                                                order.OrderType,
                                                quantity: order.AmountFirst,
                                                price: order.Price,
                                                timeInForce: order.TimeInForce);


            return new MakeOrderResult(resultPlaceOrder.Success, resultPlaceOrder.Error?.Message);
        }
    }
}
