using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Data;
using Ixcent.CryptoTerminal.Domain.Database;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Storage
{
    public static class DataSeed
    {
        public static async Task SeedDataAsync(CryptoTerminalContext context,
                                               UserManager<AppUser> userManager,
                                               RoleManager<IdentityRole> roleManager)
        {
            if (userManager.Users.Any()) return;

            IdentityRole adminRole = new() { Name = "admin" };
            IdentityRole userRole = new() { Name = "user" };

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);

            AppUser admin = new() { UserName = "admin", Email = "test@test.com" };

            await userManager.CreateAsync(admin, "Qwerty12345@");

            await userManager.AddToRoleAsync(admin, adminRole.Name);

            context.AvailableExchanges.Add(new AvailableExchangeEntity
            {
                Id = 0,
                Name = "Binance"
            });
        }
    }
}
