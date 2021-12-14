using System.Linq;

namespace CryptoTerminal.Models.CryptoExchanges
{
    public abstract class CryptoExchange
    {
        public abstract List<CoinBalance> GetCoinBalances();

        public abstract List<Order> GetOpenOrders();

        public abstract List<Order> GetOrderHistory();

        public List<CoinBalance> GetFreeCoinBalances()
        {
            // TODO
            throw new NotImplementedException();
        }

        public abstract MakeOrderResult MakeOrder(Order order);

        public abstract void CancelOrder(Order order);

        public MakeGridResult MakeGrid(List<Order> orders)
        {
            if (orders.Any(order => !string.Equals(order.Pair, orders.First().Pair)))
            {
                return new MakeGridResult(false, "All orders must be for one pair.");
            }

            CoinBalance freeBalance = GetFreeCoinBalances().First(balance => 
                                        string.Equals(balance.ShortName, orders.First().SecondCoin, StringComparison.OrdinalIgnoreCase));
            
            if (orders.Sum(order => order.Amount * order.Price) >= freeBalance.Amount)
            {
                return new MakeGridResult(false, $"Not enough {freeBalance.ShortName}.");
            }

            for(int i = 0; i < orders.Count; i++)
            {
                MakeOrderResult result = MakeOrder(orders[i]);

                if (!result.Success)
                {
                    for(int cancelIterator = 0; cancelIterator < i; cancelIterator++)
                    {
                        CancelOrder(orders[i]);
                    }
                    return new MakeGridResult(false, result.Message);
                }
            }

            return new MakeGridResult(true);
        }
    }
}
