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
    public class TwapFuturesOrder : FuturesOrder
    {
        public TwapFuturesOrder(string symbol, decimal quantity, decimal stepSize, DateTime startTime, TimeSpan duration, OrderSide orderSide, PositionSide positionSide)
            : base(symbol, quantity, orderSide, OrderType.Market, positionSide, startTime, 0, 0, TimeInForce.GoodTillCancel)
        {
            StepSize = stepSize;
            Duration = duration;
            StartTime = startTime;
        }

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

    }
}
