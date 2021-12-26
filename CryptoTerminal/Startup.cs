using CryptoExchange.Net.ExchangeInterfaces;
using CryptoTerminal.Models.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CryptoTerminal.Models.CryptoExchanges.BinanceRealisation;
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

            BinanceCryptoExchange exch = new BinanceCryptoExchange(
                "ZOcjoqRfQ86zSYz4vUyzQ4Hk63TilQGzMGskHp7d2Goc3TvCeoyHocuUo4EdAsp0",
                "iou3etuXmQYi7XWa666K7idpfNuvU3ucidwCvpWQ9v3FZURosrh62LFoRhJXVepk");

            var callOrdersHistory = exch.GetFutures().First().GetOrdersHistory().Result;

            foreach (var order in callOrdersHistory.OrderBy(ord => ord.TradeTime))
            {
                Console.WriteLine($"{order.Symbol} - {order.Price} - {order.TradeTime}");
            }

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
