using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data 
{
    using CryptoExchanges.Enums;
    public class VirtualFuturesOrder : FuturesOrder
    {
        public VirtualFuturesOrder(decimal activationPrice, string symbol, decimal amount, OrderSide orderSide, OrderType orderType, PositionSide positionSide, DateTime? date, long? id = null, decimal? price = null, TimeInForce? tif = null, bool? reduceOnly = false)
            : base(symbol, amount, orderSide, orderType, positionSide, date, id, price, tif, reduceOnly)
        {
            PlaceOrderPrice = activationPrice;
        }

        public decimal PlaceOrderPrice { get; set; }
    }
}
