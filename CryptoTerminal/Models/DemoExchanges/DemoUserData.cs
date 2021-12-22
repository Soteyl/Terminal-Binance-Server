using CryptoTerminal.Models.CryptoExchanges;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoUserData
    {
        public DemoUserData()
        {   }

        public List<SpotOrder> OpenOrders { get; set; } = new List<SpotOrder>();

        public List<CoinBalance> CoinBalances { get; set; } = new List<CoinBalance>();

        public List<SpotOrder> OrdersHistory { get; set; } = new List<SpotOrder>();

        public void AddCoinsToBalance(CoinBalance coins)
        {
            var coinBalance = CoinBalances.Find(coin => string.Equals(coin.ShortName, coins.ShortName));
            if (coinBalance != null)
            {
                coinBalance.Amount += coins.Amount;
                return;
            }
            CoinBalances.Add(coins);
        }

        public bool TrySubstractCoinsFromBalance(CoinBalance coins)
        {
            var coinBalance = CoinBalances.Find(coin => string.Equals(coin.ShortName, coins.ShortName));
            if (coinBalance != null && coinBalance.Amount >= coins.Amount)
            {
                coinBalance.Amount -= coins.Amount;
                return true;
            }
            return false;
        }

    }

}