namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{

    using Enums;

    public class FuturesTrade
    {

        public FuturesTrade(string symbol, decimal price, decimal quantity, decimal realizedPnl, decimal commission, OrderSide orderSide, PositionSide positionSide, DateTime tradeTime, long id, long orderId, bool buyer)
        {
            Symbol = symbol;
            Price = price;
            Quantity = quantity;
            RealizedPnl = realizedPnl;
            Comission = commission;
            Id = id;
            OrderId = orderId;
            Side = orderSide;
            Position = positionSide;
            TradeTime = tradeTime;
            Buyer = buyer;
        }

        public string Symbol { get; }

        public bool Buyer { get; } 

        public decimal Quantity { get; }
        
        public decimal Price { get; }

        public decimal RealizedPnl { get; }

        public decimal Comission { get; }

        public long Id { get; }

        public long OrderId { get; }

        public OrderSide Side { get; }
        
        public PositionSide Position { get; }

        public DateTime TradeTime { get; }
    }
}