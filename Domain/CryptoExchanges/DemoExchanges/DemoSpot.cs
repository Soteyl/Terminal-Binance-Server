using CryptoExchange.Net.ExchangeInterfaces;
using Binance.Net.Interfaces.SubClients.Spot;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using Data;
    using Enums;
    using Results;

    public class DemoSpot : ICryptoSpot
    {
        private IBinanceClientSpot _spot;
        private IExchangeClient _exClient;
        private IAccessDemoStorage _demoStorage;
        
        private string _userKey;

        public DemoSpot(IAccessDemoStorage demoStorage, IExchangeClient exClient, IBinanceClientSpot spot, string key)
        {
            _spot = spot;
            _userKey = key;
            _demoStorage = demoStorage;
            _exClient = exClient;
        }

        public Task CancelOrder(ICommonOrder order)
        {
            // TODO add order canceletion to the history
            _demoStorage.RemoveUserOrders(_userKey, order);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CoinBalance>> GetCoinBalances()
        {
            return Task.FromResult(_demoStorage.GetUserCoinBalances(_userKey));
        }

        public Task<IEnumerable<BookPrice>> GetCoinPairs()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry>> GetDepthOfMarket(string pair)
        {
            var res = await Task.Run(() => _exClient.GetOrderBookAsync(pair));

            if (!res.Success)
                return new List<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry>();

            var orders = res.Data;

            // TODO replace with OrderBook class

            var allBids = orders.CommonBids;
            var allAsks = orders.CommonAsks; 

            var fullDepthResult = allBids.Concat(allAsks);

            lock (fullDepthResult)
            {
                return fullDepthResult.ToList(); 
            }
        }
        
        public Task<IEnumerable<ICommonOrder>> GetOpenOrders()
        {
            return Task.FromResult(_demoStorage.GetUserOpenOrders(_userKey));
        }

        public Task<IEnumerable<ICommonOrder>> GetOrderHistory()
        {
            return Task.FromResult(_demoStorage.GetUserOrdersHistory(_userKey));
        }

        public Task<IEnumerable<ICommonTrade>> GetTransactionsHistory()
        { 
            throw new NotImplementedException();
        }

        public async Task<MakeOrderResult> MakeOrder(ICommonOrder order)
        {
            var coinBalance = (await GetCoinBalances()).First(coin => order.CommonSymbol.EndsWith(coin.ShortName));
            decimal cost = order.CommonQuantity;

            if (order.CommonSide == IExchangeClient.OrderSide.Buy)
                cost = cost * order.CommonPrice;

            if (coinBalance == null || coinBalance.Free < order.CommonQuantity)
                return new MakeOrderResult(false, "Insufficient funds!");

            _demoStorage.TryFullfillLimitOrder(_userKey, order.CommonPrice, order);

            return new MakeOrderResult(true, "Successfully placed new order!");
        }
    }
}
