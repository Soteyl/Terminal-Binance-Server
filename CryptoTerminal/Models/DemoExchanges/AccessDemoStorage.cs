using CryptoTerminal.Models.CryptoExchanges;
using System.Collections;
using System.Collections.Generic;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class AccessDemoStorage : IAccessDemoStorage
    {
        private Dictionary<string, DemoUserData> _userData;

        private DemoUserData GetDemoUserData(string key)
        {
            if (_userData.TryGetValue(key, out DemoUserData? data))
            {
                return data;
            }

            data = new DemoUserData();
            data.CoinBalances.Add(new CoinBalance("USDT", "Tether", 5000));

            _userData.Add(key, data);

            return data;
        }
        
        public AccessDemoStorage()
        {
            _userData = new Dictionary<string, DemoUserData>();
        }

        public List<SpotOrder> GetUserOpenOrders(string key)
        {
            return GetDemoUserData(key).OpenOrders;
        }

        public List<SpotOrder> GetUserOrdersHistory(string key)
        {
            return GetDemoUserData(key).OrdersHistory;
        }

        public List<CoinBalance> GetUserCoinBalances(string key)
        {
            return GetDemoUserData(key).CoinBalances.ConvertAll(bal => (CoinBalance)bal.Clone());
        }

        public void RemoveUserOrders(string key, params SpotOrder[] orders)
        {
            foreach (var order in orders)
            {
                GetDemoUserData(key).OpenOrders.Remove(order);
                GetDemoUserData(key).OrdersHistory.Add(order);
            }
        }

        public bool TryFullfillMarketOrder(string key, decimal actualPrice, SpotOrder order)
        {
            CoinBalance coinsToSubstract = new CoinBalance(order.SecondCoin, "", order.AmountFirst * actualPrice); 
            CoinBalance coinsToAdd = new CoinBalance(order.FirstCoin, "", order.AmountFirst); 

            if (order.OrderSide == OrderSide.Sell)
            {
                coinsToSubstract = new CoinBalance(order.FirstCoin, "", order.AmountFirst);
                coinsToAdd = new CoinBalance(order.SecondCoin, "", order.AmountFirst * actualPrice);
            }

            var successfullySubstracted = GetDemoUserData(key).TrySubstractCoinsFromBalance(coinsToSubstract);
            if (successfullySubstracted)
            {
                GetDemoUserData(key).AddCoinsToBalance(coinsToAdd);
                return true;
            }
            return false;
        }

        public bool TryFullfillLimitOrder(string key, decimal marketPrice, SpotOrder order)
        {
            var limitPrice = order.Price;

            if (order.OrderSide == OrderSide.Buy && limitPrice >= marketPrice ||
                order.OrderSide == OrderSide.Sell && limitPrice <= marketPrice)
            {
                return TryFullfillMarketOrder(key, marketPrice, order);
            }
            return false;   
        }

        public Dictionary<string, DemoUserData> GetAllUserData()
        {
            return _userData;
        }
    }
}
