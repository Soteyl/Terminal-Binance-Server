using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models.DemoExchanges
{
    public interface IAccessDemoStorage
    {
        List<SpotOrder> GetUserOpenOrders(string key);
        
        List<SpotOrder> GetUserOrdersHistory(string key);
        
        List<CoinBalance> GetUserCoinBalances(string key);

        Dictionary<string,DemoUserData> GetAllUserData();

        void RemoveUserOrders(string key, params SpotOrder[] orders);

        bool TryFullfillMarketOrder(string key, decimal actualPrice, SpotOrder order);

        bool TryFullfillLimitOrder(string key, decimal actualPrice, SpotOrder order);


    }
}