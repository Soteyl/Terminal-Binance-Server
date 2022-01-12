using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.EFData
{   
    public class BinanceExchangeContext : DbContext, IBinanceFuturesExchangeContext
    {
        public DbSet<TwapOrderRecord> TwapOrderRecords { get; set; }
        
        public BinanceExchangeContext(DbContextOptions<BinanceExchangeContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
