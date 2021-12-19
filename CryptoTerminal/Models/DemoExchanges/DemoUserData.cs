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

    }

}