namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    public class OrderBookEntry
    {
        public OrderBookEntry(decimal quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }

        public decimal Quantity
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
    }
}
