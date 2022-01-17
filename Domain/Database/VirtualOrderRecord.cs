using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.Database
{
    using CryptoExchanges.Data;
    public class VirtualOrderRecord
    {
        public VirtualOrderRecord(decimal placePrice, FuturesOrder orderToPlace)
        {
            PlacePrice = placePrice;
            OrderToPlace = orderToPlace;
        }

        public long Id { get; set; }

        public AppUser User { get; set; }

        public string Exchange { get; set; }

        public decimal PlacePrice { get; set; }

        public FuturesOrder OrderToPlace { get; set; }
    }
}
