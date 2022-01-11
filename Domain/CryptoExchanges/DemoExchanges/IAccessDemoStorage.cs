using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using Data;

    public interface IAccessDemoStorage
    {
        IEnumerable<ICommonOrder> GetUserOpenOrders(string key);

        IEnumerable<ICommonOrder> GetUserOrdersHistory(string key);

        IEnumerable<CoinBalance> GetUserCoinBalances(string key);

        Dictionary<string, DemoUserData> GetAllUserData();

        void RemoveUserOrders(string key, params ICommonOrder[] orders);

        bool TryFullfillMarketOrder(string key, decimal actualPrice, ICommonOrder order);

        bool TryFullfillLimitOrder(string key, decimal actualPrice, ICommonOrder order);
    }
}