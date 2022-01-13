using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data 
{
    using CryptoExchanges.Data;
    public class VirtualOrder 
    {
        public VirtualOrder(decimal activationPrice, FuturesOrder orderToPlace)
        {
            ActivationPrice = activationPrice;
            OrderToPlace = orderToPlace;
        }

        public FuturesOrder OrderToPlace { get; set; }

        public decimal ActivationPrice { get; set; }
    }
}
