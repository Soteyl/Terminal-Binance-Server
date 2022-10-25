using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Storage
{
    /// <summary>
    /// Main database context.
    /// </summary>
    /// <remarks>
    /// <see cref="ExchangeTokens"/> is responsible for storing api tokens with different exchanges.
    /// </remarks>
    public sealed class CryptoTerminalContext : IdentityDbContext<AppUser>
    {
        public DbSet<ExchangeTokenEntity> ExchangeTokens { get; set; }

        public DbSet<AvailableExchangeEntity> AvailableExchanges { get; set; }

        public CryptoTerminalContext(DbContextOptions<CryptoTerminalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public static class CryptoTerminalContextExtensions
    {
        public static async Task<ExchangeTokenEntity?> GetBinanceToken(this DbSet<ExchangeTokenEntity> source, string userId)
        {
            return await source.FirstOrDefaultAsync(t => t.UserId == userId &&
                                                    t.Exchange == "Binance");
        }
    }
}
