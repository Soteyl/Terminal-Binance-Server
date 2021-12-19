using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CryptoTerminal.Models.DemoExchanges;
using CryptoTerminal.Models.CryptoExchanges;
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
            services.AddControllersWithViews();
            services.AddTransient<IAccessDemoStorage, AccessDemoStorage>();
            services.AddTransient<DemoExchange>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DemoExchange de)
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

            var dateTime = DateTime.Now;

            de.GetCryptoSpot().MakeOrder(new SpotOrder("BTC/USDT", 0.002m, 48000, OrderSide.Buy, OrderType.Market, dateTime));

            foreach (var res in de.GetCryptoSpot().GetDepthOfMarket("XLMUSDT"))
            {
                Console.WriteLine(res);
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
