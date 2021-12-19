﻿using CryptoExchange.Net.ExchangeInterfaces;

namespace CryptoTerminal.Models.CryptoExchanges
{
    public abstract class CryptoSpot
    {
        public abstract Task<IEnumerable<CoinBalance>> GetCoinBalances();

		public abstract IEnumerable<BookPrice> GetCoinPairs();

        public abstract Task<IEnumerable<SpotOrder>> GetOpenOrders();

        public abstract Task<IEnumerable<ICommonOrder>> GetOrderHistory();

        public abstract Task<IEnumerable<ICommonTrade>> GetTransactionsHistory();

        public abstract Task<OrderBook> GetDepthOfMarket(string symbol);

        public abstract Task<MakeOrderResult> MakeOrder(SpotOrder order);

        public abstract void CancelOrder(SpotOrder order);

        public async Task<MakeGridResult> MakeGrid(List<SpotOrder> orders)
        {
            // TODO
            throw new NotImplementedException();

            //if (orders.Any(order => !string.Equals(order.Pair, orders.First().Pair)))
            //{
            //    return new MakeGridResult(false, "All orders must be for one pair.");
            //}

            //CoinBalance freeBalance = GetFreeCoinBalances().First(balance =>
            //                            string.Equals(balance.ShortName, orders.First().SecondCoin, StringComparison.OrdinalIgnoreCase));

            //if (orders.Sum(order => order.AmountFirst * order.Price) >= freeBalance.Amount)
            //{
            //    return new MakeGridResult(false, $"Not enough {freeBalance.ShortName}.");
            //}

            //for (int i = 0; i < orders.Count; i++)
            //{
            //    MakeOrderResult result = await MakeOrder(orders[i]);

            //    if (!result.Success)
            //    {
            //        for (int cancelIterator = 0; cancelIterator < i; cancelIterator++)
            //        {
            //            CancelOrder(orders[i]);
            //        }
            //        return new MakeGridResult(false, result.Message);
            //    }
            //}

            //return new MakeGridResult(true);
        }
    }
}