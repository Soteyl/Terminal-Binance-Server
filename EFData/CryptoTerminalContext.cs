using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;

    public class CryptoTerminalContext: IdentityDbContext<AppUser>, IBinanceFuturesExchangeContext
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
            modelBuilder.Entity<TwapOrderRecord>()
                .Property(p => p.OrderSide)
                .HasConversion(
                    p => p.Value,
                    p => (Domain.CryptoExchanges.Enums.OrderSide)Domain.CryptoExchanges.Enums.OrderSide.Values.GetValueOrDefault(p)
                );
            modelBuilder.Entity<TwapOrderRecord>()
                .Property(p => p.PositionSide)
                .HasConversion(
                    p => p.Value,
                    p => (Domain.CryptoExchanges.Enums.PositionSide)Domain.CryptoExchanges.Enums.PositionSide.Values.GetValueOrDefault(p)
                );
        }
    }
}
