using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;
    using Domain.CryptoExchanges.Data;

    public class CryptoTerminalContext : IdentityDbContext<AppUser>, IFuturesExchangeContext
    {
        public CryptoTerminalContext(DbContextOptions<CryptoTerminalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TwapOrderRecord> TwapFuturesOrderRecords { get; set; }

        public DbSet<VirtualOrderRecord> VirtualFuturesOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
