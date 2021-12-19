using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;
using System.Linq;
using System.Text.Json;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Interfaces;
using Binance.Net.Objects;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoSpot : CryptoSpot
    {
        private IBinanceClientSpot _spot;
        private IAccessDemoStorage _demoStorage;

        private string _userKey;

        // TODO separate storage from spot exchange.

        public DemoSpot(IAccessDemoStorage demoStorage, IBinanceClientSpot spot, string key)
        {
            _spot = spot;
            _userKey = key;
            _demoStorage = demoStorage;
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

        public override List<SpotOrder> GetDepthOfMarket(string pair)
        {
            var query = _spot.Market.GetOrderBookAsync(pair);

            var orders = query.GetAwaiter().GetResult();

            // TODO fix booked orders request
            return new List<SpotOrder>(); 
            /* orders.Data.Select(binanceOrder => new SpotOrder(
                pair,
                binanceOrder.Quantity,
                binanceOrder.Price,
                (OrderSide)(int) binanceOrder.Side,
                (OrderType)(int) binanceOrder.Type
            )).ToList();*/
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
            if (order.OrderSide == OrderSide.Buy)
            {
                var coinBalance = GetCoinBalances().Find(c => string.Equals(order.SecondCoin, c.ShortName));
                var cost = order.AmountFirst * order.Price;

                if (coinBalance == null || coinBalance.Amount < cost)
                {
                    return new MakeOrderResult(0, false, "Insufficient funds!");
                }
            }
            else if (order.OrderSide == OrderSide.Sell)
            {
                var coinBalance = GetCoinBalances().Find(c => string.Equals(order.SecondCoin, c.ShortName));

                if (coinBalance == null || coinBalance.Amount < order.AmountFirst)
                {
                    return new MakeOrderResult(0, false, "Insufficient funds!");
                }
            }
    
            GetOpenOrders().Add(order);
            return new MakeOrderResult(0, true, "Successfully placed new order!");
        }
    }
}
