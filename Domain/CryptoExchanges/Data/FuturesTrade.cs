using Binance.Net.Objects.Futures.FuturesData;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{

    using Enums;

    public class FuturesTrade
    {
        public string Symbol { get; set; } = string.Empty;

        public bool Buyer { get; set; } 

        public decimal Quantity { get; set; }
        
        public decimal Price { get; set; }

        public decimal RealizedPnl { get; set; }

        public decimal Commission { get; set;  }

        public long Id { get; set; }

        public long OrderId { get; set; }

        public OrderSide Side { get; set; }
        
        public PositionSide PositionSide { get; set; }

        public DateTime TradeTime { get; set; }

        public static implicit operator FuturesTrade(BinanceFuturesUsdtTrade bal)
        {
            return new FuturesTrade
            {
                Symbol = bal.Symbol,
                Quantity = bal.Quantity,
                Price = bal.Price,
                Buyer = bal.Buyer,
                RealizedPnl = bal.RealizedPnl,
                Commission = bal.Commission,
                Id = bal.Id,
                Side = bal.Side,
                PositionSide = bal.PositionSide,
                TradeTime = bal.TradeTime
            };
        }

        public static implicit operator BinanceFuturesUsdtTrade(FuturesTrade bal)
        {
            return bal;
        }
    }
}