namespace CryptoTerminal.Models.CryptoExchanges
{
    public abstract class CryptoSpot
    {
        public abstract IEnumerable<BookPrice> GetCoinPairs();

        public abstract List<CoinBalance> GetCoinBalances();

        public abstract List<SpotOrder> GetOpenOrders();

        public abstract List<SpotOrder> GetOrderHistory();

        public abstract List<Transaction> GetTransactionsHistory();

        public abstract List<SpotOrder> GetDepthOfMarket();

        public List<CoinBalance> GetFreeCoinBalances()
        {
            var balances = GetCoinBalances();
            var openOrders = GetOpenOrders();

            foreach(var balance in balances)
            {
                foreach(var order in openOrders)
                {
                    if (order.FirstCoin == balance.ShortName && order.OrderSide == OrderSide.Sell)
                    {
                        balance.Amount -= order.AmountFirst;
                    }
                    else if (order.SecondCoin == balance.ShortName && order.OrderSide == OrderSide.Buy)
                    {
                        balance.Amount -= order.AmountFirst * order.Price;
                    }
                }
            }

            return balances;
        }

        public abstract Task<MakeOrderResult> MakeOrder(SpotOrder order);

        public abstract void CancelOrder(SpotOrder order);

        public async Task<MakeGridResult> MakeGrid(List<SpotOrder> orders)
        {
            // TODO
            throw new NotImplementedException();

            if (orders.Any(order => !string.Equals(order.Pair, orders.First().Pair)))
            {
                return new MakeGridResult(false, "All orders must be for one pair.");
            }

            CoinBalance freeBalance = GetFreeCoinBalances().First(balance =>
                                        string.Equals(balance.ShortName, orders.First().SecondCoin, StringComparison.OrdinalIgnoreCase));

            if (orders.Sum(order => order.AmountFirst * order.Price) >= freeBalance.Amount)
            {
                return new MakeGridResult(false, $"Not enough {freeBalance.ShortName}.");
            }

            for (int i = 0; i < orders.Count; i++)
            {
                MakeOrderResult result = await MakeOrder(orders[i]);

                if (!result.Success)
                {
                    for (int cancelIterator = 0; cancelIterator < i; cancelIterator++)
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