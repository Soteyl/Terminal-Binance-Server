using CryptoExchange.Net.ExchangeInterfaces;
namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    public class BookPrice
    {
        public string Pair
        {
            get;
            set;
        } = string.Empty;

        public decimal BestBidPrice
        {
            get;
            set;
        }

        public decimal BestBidQuantity
        {
            get;
            set;
        }

        public decimal BestAskPrice
        {
            get;
            set;
        }

        public decimal BestAskQuantity
        {
            get;
            set;
        }

        public DateTime? Timestamp
        {
            get;
            set;
        }
    }

    public static class BookPriceExtensions
    {
        public static BookPrice ToIxcentBookPrice(this Binance.Net.Objects.Spot.MarketData.BinanceBookPrice data)
        {
            return new BookPrice()
            {
                Pair = data.Symbol,
                BestBidPrice = data.BestBidPrice,
                BestAskQuantity = data.BestAskQuantity,
                BestBidQuantity = data.BestBidQuantity,
                BestAskPrice = data.BestAskPrice,
                Timestamp = data.Timestamp
            };
        }
    }
}
