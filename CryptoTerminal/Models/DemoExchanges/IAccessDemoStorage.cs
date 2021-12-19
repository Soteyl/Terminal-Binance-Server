using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models.DemoExchanges
{
    public interface IAccessDemoStorage
    {
        List<SpotOrder> GetUserOpenOrders(string key);
        List<SpotOrder> GetUserOrdersHistory(string key);
        List<CoinBalance> GetUserCoinBalances(string key);

    }
}