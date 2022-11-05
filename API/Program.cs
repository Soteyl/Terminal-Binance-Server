using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Storage;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;

namespace Ixcent.CryptoTerminal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Ixcent", "CryptoTerminal", "logs", ".log"),
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                IHost? host = CreateHostBuilder(args).Build();

                RefreshDatabase(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void RefreshDatabase(IHost host)
        {
            using IServiceScope? scope = host.Services.CreateScope();
            IServiceProvider? services = scope.ServiceProvider;
            try
            {
                CryptoTerminalContext? context = services.GetRequiredService<CryptoTerminalContext>();
                UserManager<AppUser>? userManager = services.GetRequiredService<UserManager<AppUser>>();
                RoleManager<IdentityRole>? roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                context.Database.Migrate();
                DataSeed.SeedDataAsync(context, userManager, roleManager).Wait();
            }
            catch (Exception ex)
            {
                ILogger<Program>? logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseKestrel();
                });
    }
}
