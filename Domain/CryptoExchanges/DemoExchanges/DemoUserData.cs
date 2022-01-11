using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using Data;

    public class DemoUserData
    {
        public DemoUserData()
        { }

        public List<ICommonOrder> OpenOrders { get; set; } = new List<ICommonOrder>();

        public List<CoinBalance> CoinBalances { get; set; } = new List<CoinBalance>();

        public List<ICommonOrder> OrdersHistory { get; set; } = new List<ICommonOrder>();

        public void AddCoinsToBalance(CoinBalance coins)
        {
            var coinBalance = CoinBalances.Find(coin => string.Equals(coin.ShortName, coins.ShortName));
            if (coinBalance != null)
            {
                coinBalance.Free += coins.Total;
                return;
            }
            CoinBalances.Add(coins);
        }

        public bool TrySubstractCoinsFromBalance(CoinBalance coins)
        {
            var coinBalance = CoinBalances.Find(coin => string.Equals(coin.ShortName, coins.ShortName));
            if (coinBalance != null && coinBalance.Free >= coins.Total)
            {
                coinBalance.Free -= coins.Total;
                return true;
            }
            return false;
        }

    }

}