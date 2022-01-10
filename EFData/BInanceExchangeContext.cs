using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Database;


namespace Ixcent.CryptoTerminal.EFData
{   
    public class BinanceExchangeContext : DbContext
    {
        public DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }
        
        public BinanceExchangeContext(DbContextOptions<BinanceExchangeContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
