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
            var coin = CoinBalances.Find(c => string.Equals(c.ShortName, coins.ShortName));
            if (coin != null)
            {
                coin.Amount += coins.Amount;
                return;
            }
            CoinBalances.Add(coins);
        }

        public bool TrySubstractCoinsFromBalance(CoinBalance coins)
        {
            var coin = CoinBalances.Find(c => string.Equals(c.ShortName, coins.ShortName));
            if (coin != null && coin.Amount >= coins.Amount)
            {
                coin.Amount -= coins.Amount;
                return true;
            }
            return false;
        }

    }

}