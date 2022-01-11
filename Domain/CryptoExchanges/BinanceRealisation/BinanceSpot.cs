using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.BinanceRealisation
{
    using Data;
    using Results;
    /// <summary>
    /// Implementation of Binance Spot instrument. <para/>
    /// 
    /// Inherited from <see cref="CryptoSpot"/>
    /// </summary>
    public class BinanceSpot : ICryptoSpot
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

        public Task CancelOrder(ICommonOrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CoinBalance>> GetCoinBalances()
        {
            WebCallResult<BinanceAccountInfo> info = await _general.GetAccountInfoAsync();

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return balances.Select(balance => new CoinBalance(balance.Asset, balance.Free, balance.Locked));
        }

        public async Task<IEnumerable<BookPrice>> GetCoinPairs()
        {
            WebCallResult<IEnumerable<BinanceBookPrice>> resultBookPrices = await _spot.Market.GetAllBookPricesAsync();

            return resultBookPrices.Data.Select(a => a.ToIxcentBookPrice());
        }

        public async Task<IEnumerable<ISymbolOrderBookEntry>> GetDepthOfMarket(string symbol)
        {
            WebCallResult<ICommonOrderBook> resultOrderBook = await _exchangeClient.GetOrderBookAsync(symbol);
            return resultOrderBook.Data.CommonBids.Concat(resultOrderBook.Data.CommonAsks);
        }

        public async Task<IEnumerable<ICommonOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceOrder>> resultBinanceOrders = await _spot.Order.GetOpenOrdersAsync();

            return resultBinanceOrders.Data;
        }

        public async Task<IEnumerable<ICommonOrder>> GetOrderHistory()
        {
            var orderHistory = new List<ICommonOrder>();
            IEnumerable<ICommonSymbol> symbols = (await _exchangeClient.GetSymbolsAsync()).Data;
            foreach (ICommonSymbol symbol in symbols)
            {
                orderHistory.AddRange((await _exchangeClient.GetClosedOrdersAsync(symbol.CommonName)).Data);
            }
            return orderHistory;
        }

        public async Task<IEnumerable<ICommonTrade>> GetTransactionsHistory()
        {
            var transactionHistory = new List<ICommonTrade>();
            IEnumerable<ICommonOrder> orders = await GetOrderHistory();
            foreach (ICommonOrder order in orders)
            {
                transactionHistory.AddRange((await _exchangeClient.GetTradesAsync(order.CommonId, order.CommonSymbol)).Data);
            }
            return transactionHistory;
        }

        public async Task<MakeOrderResult> MakeOrder(ICommonOrder order)
        {
            var spotOrder = order as SpotOrder;
            if (spotOrder == null) throw new Exception("order must be SpotOrder class");

            WebCallResult<BinancePlacedOrder> resultPlaceOrder =
              await _spot.Order.PlaceOrderAsync(spotOrder.Pair,
                                                spotOrder.OrderSide,
                                                spotOrder.OrderType,
                                                quantity: spotOrder.AmountFirst,
                                                price: spotOrder.Price,
                                                timeInForce: spotOrder.TimeInForce!);


            return new MakeOrderResult(resultPlaceOrder.Success, resultPlaceOrder.Error?.Message);
        }
    }
}
