using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    using Data;
    using Results;

    public abstract class CryptoSpot
    {
        public abstract Task<IEnumerable<CoinBalance>> GetCoinBalances();

		public abstract Task<IEnumerable<BookPrice>> GetCoinPairs();

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
        }
    }
}