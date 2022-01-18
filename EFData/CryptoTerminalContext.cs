using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Interfaces;
using Newtonsoft.Json;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;
    using Domain.Converters;
    using Domain.CryptoExchanges.Enums;
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
            modelBuilder.Entity<VirtualOrderRecord>()
                .Property(p => p.OrderToPlace)
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity, new FuturesOrderConverter()),
                    value => JsonConvert.DeserializeObject<FuturesOrder>(value, new FuturesOrderConverter())
                );
            modelBuilder.Entity<TwapOrderRecord>()
                .Property(p => p.PositionSide)
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity, new AdvancedEnumConverter()),
                    value => JsonConvert.DeserializeObject<PositionSide>(value, new AdvancedEnumConverter())
                );
            modelBuilder.Entity<TwapOrderRecord>()
                .Property(p => p.OrderSide)
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity, new AdvancedEnumConverter()),
                    value => JsonConvert.DeserializeObject<OrderSide>(value, new AdvancedEnumConverter())
                );

        }
    }
}
