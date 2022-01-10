
namespace Ixcent.CryptoTerminal.Domain.Database
{
    using CryptoExchanges.Enums;

    public class TwapOrderRecord
    {

        public TwapOrderRecord(string symbol, decimal quantity, DateTime executeTime, PositionSide positionSide, OrderSide orderSide)
        {
            Symbol = symbol;
            Quantity = quantity;
            ExecuteTime = executeTime;
            PositionSide = positionSide;
            OrderSide = orderSide;
        }

        public int Id { get; set; }

        public string Symbol { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExecuteTime { get; set; }

        public PositionSide PositionSide { get; set; }

        public OrderSide OrderSide { get; set; }

    }
}
