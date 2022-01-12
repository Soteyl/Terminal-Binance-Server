using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    using Database;
    public interface IBinanceFuturesExchangeContext
    {
        DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }

    }
}
