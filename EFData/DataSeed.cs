using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.EFData
{
    using Domain.Database;

    public class DataSeed
    {
        public static async Task SeedDataAsync(CryptoTerminalContext context, UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any()) return;

            var admin = new AppUser { UserName = "admin", Email = "test@test.com" };

            await userManager.CreateAsync(admin, "Qwerty12345@");
        }
    }
}
