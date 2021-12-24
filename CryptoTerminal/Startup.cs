using CryptoExchange.Net.ExchangeInterfaces;
using CryptoTerminal.Models.Database;
using Binance.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTerminal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CryptoTerminalContext>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            Models.CryptoExchanges.BinanceRealisation.BinanceFutures fut 
                = new Models.CryptoExchanges.BinanceRealisation.BinanceFutures(new BinanceClient(
                        new Binance.Net.Objects.BinanceClientOptions()
                        { 
                            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(
                             "ZOcjoqRfQ86zSYz4vUyzQ4Hk63TilQGzMGskHp7d2Goc3TvCeoyHocuUo4EdAsp0",
                             "iou3etuXmQYi7XWa666K7idpfNuvU3ucidwCvpWQ9v3FZURosrh62LFoRhJXVepk"
                            )
                        }
                    ), "USDT");;

            var task = fut.GetUSDTBalance();

            var res = task.Result;

            Console.WriteLine(res.Total);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
