namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using CryptoExchange.Net.ExchangeInterfaces;
    using Data;
    using Enums;
    using Extensions;

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
            //data.CoinBalances.Add(new CoinBalance("USDT", 5000));

            _userData.Add(key, data);

            return data;
        }
        
        public AccessDemoStorage()
        {
            _userData = new Dictionary<string, DemoUserData>();
        }

        public IEnumerable<ICommonOrder> GetUserOpenOrders(string key)
        {
            return GetDemoUserData(key).OpenOrders;
        }

        public IEnumerable<ICommonOrder> GetUserOrdersHistory(string key)
        {
            return GetDemoUserData(key).OrdersHistory;
        }

        public IEnumerable<CoinBalance> GetUserCoinBalances(string key)
        {
            return GetDemoUserData(key).CoinBalances.ConvertAll(bal => bal.CopyWithSerialize()).Where(bal => bal != null)!;
        }

        public void RemoveUserOrders(string key, params ICommonOrder[] orders)
        {
            foreach (var order in orders)
            {
                GetDemoUserData(key).OpenOrders.Remove(order);
                GetDemoUserData(key).OrdersHistory.Add(order);
            }
        }

        public bool TryFullfillMarketOrder(string key, decimal actualPrice, ICommonOrder order)
        {

            //CoinBalance coinsToSubstract = new CoinBalance(order., "", order.AmountFirst * order.Price); 
            //CoinBalance coinsToAdd = new CoinBalance(order.FirstCoin, "", order.AmountFirst); 

            //if (order.OrderSide == OrderSide.Sell)
            //{
            //    // Swapping coin balances 
            //    CoinBalance temp = coinsToSubstract;
            //    coinsToSubstract = coinsToAdd;
            //    coinsToAdd = temp;
            //}

            //var successfullySubstracted = GetDemoUserData(key).TrySubstractCoinsFromBalance(coinsToSubstract);
            //if (successfullySubstracted)
            //{
            //    GetDemoUserData(key).AddCoinsToBalance(coinsToAdd);
            //    return true;
            //}
            return false;
        }

        public bool TryFullfillLimitOrder(string key, decimal marketPrice, ICommonOrder order)
        {
            var limitPrice = order.CommonPrice;

            if (order.CommonSide ==IExchangeClient.OrderSide.Buy && limitPrice >= marketPrice ||
                order.CommonSide == IExchangeClient.OrderSide.Sell && limitPrice <= marketPrice)
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
