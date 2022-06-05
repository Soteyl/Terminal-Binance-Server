using CryptoExchange.Net.ExchangeInterfaces;

using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data;
using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Results;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public abstract class CryptoSpot
    {
        public abstract Task<IEnumerable<ICommonBalance>> GetCoinBalances();

        public abstract Task<IEnumerable<BookPrice>> GetCoinPairs();

        public abstract Task<IEnumerable<ICommonOrder>> GetOpenOrders();

        public abstract Task<IEnumerable<ICommonOrder>> GetOrderHistory();

        public abstract Task<IEnumerable<ICommonTrade>> GetTransactionsHistory();

        public abstract Task<OrderBook> GetDepthOfMarket(string symbol);

        public abstract Task<MakeOrderResult> MakeOrder(SpotOrder order);

        public abstract void CancelOrder(SpotOrder order);

        public Task<MakeGridResult> MakeGrid(IEnumerable<SpotOrder> orders)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}