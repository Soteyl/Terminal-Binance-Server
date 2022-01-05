using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.API
{
    using Domain.Database;
    using EFData;

    public class Program
    {
        public static void Main(string[] args)
        {
            IHost? host = CreateHostBuilder(args).Build();

            RefreshDatabase(host);

            host.Run();
        }

        public static void RefreshDatabase(IHost host)
        {
            using (IServiceScope? scope = host.Services.CreateScope())
            {
                IServiceProvider? services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CryptoTerminalContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();
                    DataSeed.SeedDataAsync(context, userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
