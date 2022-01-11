using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    using Data;
    using Results;

    public interface ICryptoSpot
    {
        Task<IEnumerable<CoinBalance>> GetCoinBalances();

        Task<IEnumerable<BookPrice>> GetCoinPairs();

        Task<IEnumerable<ICommonOrder>> GetOpenOrders();

        Task<IEnumerable<ICommonOrder>> GetOrderHistory();

        Task<IEnumerable<ICommonTrade>> GetTransactionsHistory();

        Task<IEnumerable<ISymbolOrderBookEntry>> GetDepthOfMarket(string symbol);

        Task<MakeOrderResult> MakeOrder(ICommonOrder order);

        Task CancelOrder(ICommonOrder order);

        public async Task<MakeGridResult> MakeGrid(List<SpotOrder> orders)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}