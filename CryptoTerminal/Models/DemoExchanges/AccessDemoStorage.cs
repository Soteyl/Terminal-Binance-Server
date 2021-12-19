using CryptoTerminal.Models.CryptoExchanges;
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
    }
}
