using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models.DemoExchanges
{
    public interface IAccessDemoStorage
    {
        List<SpotOrder> GetUserOpenOrders(string key);
        
        List<SpotOrder> GetUserOrdersHistory(string key);
        
        List<CoinBalance> GetUserCoinBalances(string key);

        Dictionary<string,DemoUserData> GetAllUserData();

        bool TryFullfillOrder(string key, SpotOrder order);

    }
}