using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database.Models;
    using Domain.Database;

    /// <summary>
    /// Main database context.
    /// <see cref="_exchangesTokens"/> is responsible for storing api tokens with different exchanges.
    /// </summary>
    public class CryptoTerminalContext : IdentityDbContext<AppUser>
    {
        public DbSet<ExchangeToken> ExchangeTokens { get; set; }

        public DbSet<AvailableExchange> AvailableExchanges { get; set; }

        public CryptoTerminalContext(DbContextOptions<CryptoTerminalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
