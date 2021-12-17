using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;
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
            _openOrders.Remove(order); 
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetOpenOrders()
        {
            return _openOrders;
        }

        public override List<SpotOrder> GetOrderHistory()
        {
            return _ordersHistory;
        }

        public override List<Transaction> GetTransactionsHistory()
        {
            throw new NotImplementedException();
        }

        public override MakeOrderResult MakeOrder(SpotOrder order)
        {
            
            // Else it's limit order.
            if (order.OrderSide == OrderSide.Buy)
            {
                var coinBalance = _coinBalances.Find(c => order.SecondCoin == c.ShortName);
                var cost = order.AmountFirst * order.Price;

                if (coinBalance != null && coinBalance.Amount >= cost)
                {
                    coinBalance.Amount -= order.AmountFirst * order.Price;
                    if (order.OrderType == OrderType.Market)
                    {
                        
                        return new MakeOrderResult(0, true, "Successfully completed your order!");
                    }
                }
                else
                    return new MakeOrderResult(0, false, "Insufficient funds!");
            }
            else if (order.OrderSide == OrderSide.Sell)
            {
                var coinBalance = _coinBalances.Find(c => order.FirstCoin == c.ShortName);

                if (coinBalance != null && coinBalance.Amount >= order.AmountFirst)
                {
                    coinBalance.Amount -= order.AmountFirst;
                }
                else
                    return new MakeOrderResult(0, false, "Insufficient funds!");
            }
    
            _openOrders.Add(order);
            return new MakeOrderResult(0, true, "Successfully placed new order!");
        }
    }
}
