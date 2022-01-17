using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;

    public class CryptoTerminalContext : IdentityDbContext<AppUser>, IFuturesExchangeContext
    {
        public CryptoTerminalContext(DbContextOptions<CryptoTerminalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
