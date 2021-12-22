using System.Linq;
using System.Text.Json;
using Binance.Net;
using Binance.Net.SubClients.Spot;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects; 
using Binance.Net.Interfaces.SubClients.Spot;
using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoSpot : CryptoSpot
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

        public override void CancelOrder(SpotOrder order)
        {
            // TODO add order canceletion to the history
            _demoStorage.GetUserOpenOrders(_userKey).Remove(order); 
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            return _demoStorage.GetUserCoinBalances(_userKey);
        }

        public override async Task<IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry>> GetDepthOfMarket(string pair)
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
        
        public override List<SpotOrder> GetOpenOrders()
        {
            return _demoStorage.GetUserOpenOrders(_userKey);
        }

        public override List<SpotOrder> GetOrderHistory()
        {
            return _demoStorage.GetUserOrdersHistory(_userKey);
        }

        public override List<Transaction> GetTransactionsHistory()
        { 
            throw new NotImplementedException();
        }

        public override MakeOrderResult MakeOrder(SpotOrder order)
        {
            var coinBalance = GetFreeCoinBalances().Find(coin => string.Equals(order.SecondCoin, coin.ShortName));
            decimal cost = order.AmountFirst;

            if (order.OrderSide == OrderSide.Buy)
                cost = cost * order.Price;

            if (coinBalance == null || coinBalance.Amount < order.AmountFirst)
                return new MakeOrderResult(0, false, "Insufficient funds!");

            GetOpenOrders().Add(order);

            return new MakeOrderResult(0, true, "Successfully placed new order!");
        }
    }
}
