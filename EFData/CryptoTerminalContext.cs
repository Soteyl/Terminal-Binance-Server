using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;

    /// <summary>
    /// Main database context.
    /// </summary>
    public class CryptoTerminalContext : IdentityDbContext<AppUser>
    {
        public CryptoTerminalContext(DbContextOptions<CryptoTerminalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
