using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;
using System.Linq;
using System.Text.Json;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Interfaces;
using Binance.Net.Objects;

namespace CryptoTerminal.Models
{
    public class DemoSpot : CryptoSpot
    {
        private IBinanceClientSpot _spot;

        // TODO separate storage from spot exchange.
        private List<CoinBalance> _coinBalances = new List<CoinBalance>();
        private List<SpotOrder> _openOrders = new List<SpotOrder>();
        private List<SpotOrder> _ordersHistory = new List<SpotOrder>(); 

        public DemoSpot(IBinanceClientSpot spot)
        {
            // TODO make loading balances and progress from a storage 
            _coinBalances.Add(new CoinBalance("USDT", "Tether", 5000));
            _spot = spot;
        }

        public override void CancelOrder(SpotOrder order)
        {
            // TODO add order canceletion to the history
            _openOrders.Remove(order); 
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            return _coinBalances.ConvertAll(bal => (CoinBalance) bal.Clone());
        }

        public override List<SpotOrder> GetDepthOfMarket()
        {
            //var query = _spot.Order.GetOrdersAsync("XLM");
            //query.Wait();
            //return query.Result.Data.ToList().Select(binOrder => new SpotOrder(binOrder.P, binOrder));

            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetOpenOrders()
        {
            // TODO getting orders from a special demo exch sim service 
            return _openOrders.ToList();
        }

        public override List<SpotOrder> GetOrderHistory()
        {
            // TODO getting orders from a special demo exch sim service 
            return _ordersHistory.ToList();
        }

        public override List<Transaction> GetTransactionsHistory()
        { 
            throw new NotImplementedException();
        }

        public override MakeOrderResult MakeOrder(SpotOrder order)
        {
            if (order.OrderSide == OrderSide.Buy)
            {
                var coinBalance = _coinBalances.Find(c => string.Equals(order.SecondCoin, c.ShortName));
                var cost = order.AmountFirst * order.Price;

                if (coinBalance == null || coinBalance.Amount < cost)
                {
                    return new MakeOrderResult(0, false, "Insufficient funds!");
                }
            }
            else if (order.OrderSide == OrderSide.Sell)
            {
                var coinBalance = _coinBalances.Find(c => string.Equals(order.SecondCoin, c.ShortName));

                if (coinBalance == null || coinBalance.Amount < order.AmountFirst)
                {
                    return new MakeOrderResult(0, false, "Insufficient funds!");
                }
            }
    
            _openOrders.Add(order);
            return new MakeOrderResult(0, true, "Successfully placed new order!");
        }
    }
}
