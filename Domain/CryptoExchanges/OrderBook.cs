namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public class OrderBook
    {
        public OrderBook(IEnumerable<OrderBookEntry> commonBids, IEnumerable<OrderBookEntry> commonAsks)
        {
            CommonBids = commonBids;
            CommonAsks = commonAsks;
        }

        public IEnumerable<OrderBookEntry> CommonBids
        {
            get;
        }
        public IEnumerable<OrderBookEntry> CommonAsks
        {
            get;
        }
    }
}
