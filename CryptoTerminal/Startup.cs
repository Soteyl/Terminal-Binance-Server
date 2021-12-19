using CryptoExchange.Net.ExchangeInterfaces;
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
            services.AddControllersWithViews();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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


            string apiKey = "K4ef7qERwy61dI4XJgBkilZDAnvqM3QwkYt5nZZcQAuYvERQZL9cX05QFk2J3iWa";
            string apiSecret = "JyeOStAi3ip96dBN0ngwGoxZWA0NhFc75DOyKsT4tGrX7MnkK2tXsyTODISbEHfB";
            IExchangeClient a = new Binance.Net.BinanceClient(new Binance.Net.Objects.BinanceClientOptions() { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret) });
            foreach(var l in (await a.GetSymbolsAsync()).Data)
            {
                var b = await a.GetClosedOrdersAsync(l.CommonName);
                if (b.Data.Any())
                {
                    foreach (var order in b.Data)
                    {
                        var c = await a.GetTradesAsync(order.CommonId, order.CommonSymbol);
                    }
                }
                
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
