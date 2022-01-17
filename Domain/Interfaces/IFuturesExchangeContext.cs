using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    using CryptoExchanges.Data;
    using Database;
    public interface IFuturesExchangeContext
    {
        DbSet<TwapOrderRecord> TwapFuturesOrderRecords { get; set; }

        DbSet<VirtualOrderRecord> VirtualFuturesOrders { get; set; }

    }
}
