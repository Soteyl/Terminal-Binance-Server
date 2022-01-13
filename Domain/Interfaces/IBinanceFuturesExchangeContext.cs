using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    using CryptoExchanges.Data;
    using Database;
    public interface IBinanceFuturesExchangeContext
    {
        DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }
        DbSet<VirtualOrder> VirtualOrderRecords { get; set; }

    }
}
