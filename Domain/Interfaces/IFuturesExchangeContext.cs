using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    using CryptoExchanges.Data;
    using Database;
    public interface IFuturesExchangeContext
    {
        DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }

    }
}
