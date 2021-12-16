using CryptoTerminal.Models.CryptoExchanges;
using Binance.Net;

namespace CryptoTerminal.Models
{
    public class DemoSpot : CryptoExchanges.CryptoSpot
    {
        

        public override void CancelOrder(SpotOrder order)
        {
            
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetOrderHistory()
        {
            throw new NotImplementedException();
        }

        public override List<Transaction> GetTransactionsHistory()
        {
            throw new NotImplementedException();
        }

        public override MakeOrderResult MakeOrder(SpotOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
