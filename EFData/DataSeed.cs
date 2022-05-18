using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;
    using Domain.Database.Models;

    public class DataSeed
    {
        public static async Task SeedDataAsync(CryptoTerminalContext context,
                                               UserManager<AppUser> userManager,
                                               RoleManager<IdentityRole> roleManager)
        {
            if (userManager.Users.Any()) return;

            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);

            var admin = new AppUser { UserName = "admin", Email = "test@test.com" };

            await userManager.CreateAsync(admin, "Qwerty12345@");

            await userManager.AddToRoleAsync(admin, adminRole.Name);

            context.AvailableExchanges.Add(new AvailableExchange
            {
                Id = 0,
                Name = "Binance"
            });

        }
    }
}
