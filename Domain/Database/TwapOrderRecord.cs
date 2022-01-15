
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.Database
{
    using CryptoExchanges.Enums;

    public class TwapOrderRecord
    {
        private static long _id = 0;
        public TwapOrderRecord(string symbol, decimal quantity, DateTime executeTime, PositionSide positionSide, OrderSide orderSide)
        {
            Symbol = symbol;
            Quantity = quantity;
            ExecuteTime = executeTime;
            PositionSide = positionSide;
            OrderSide = orderSide;
            Id = _id++;
        }

        public long Id { get; set; }

        public AppUser User { get; set; }

        public string Exchange { get; set; }

        public string Symbol { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExecuteTime { get; set; }

        public PositionSide PositionSide { get; set; }

        public OrderSide OrderSide { get; set; }

    }
}
