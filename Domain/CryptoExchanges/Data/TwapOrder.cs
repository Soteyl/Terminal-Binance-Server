using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    using Enums;
    /// <summary>
    /// TWAP strategy order.
    /// </summary>
    public class TwapOrder
    {
        public TwapOrder(string symbol, decimal quantity, decimal stepSize, DateTime startTime, TimeSpan duration, OrderSide orderSide, PositionSide positionSide)
        {
            Symbol = symbol;
            Quantity = quantity;
            StepSize = stepSize;
            Duration = duration;
            StartTime = startTime;
            Position = positionSide;
            Side = orderSide;
        }

        /// <summary>
        /// Trade symbol.
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Amount to be released by the order.
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Step of quantity to release per interval.
        /// </summary>
        public decimal StepSize { get; set; }
        /// <summary>
        /// How much time would it take order to continue.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// The beginning of the order.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Position side.
        /// </summary>
        public PositionSide Position { get; set; }
        /// <summary>
        /// Order side (sell/buy).
        /// </summary>
        public OrderSide Side { get; set; }

    }
}
