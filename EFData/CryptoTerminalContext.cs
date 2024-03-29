﻿using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Database.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.EFData
{
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

    public static class CryptoTerminalContextExtensions
    {
        public static async Task<ExchangeToken?> GetBinanceToken(this DbSet<ExchangeToken> source, string userId)
        {
            return await source.FirstOrDefaultAsync(t => t.UserId == userId &&
                                                    t.Exchange == "Binance");
        }
    }
}
