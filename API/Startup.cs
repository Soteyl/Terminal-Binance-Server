using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


namespace Ixcent.CryptoTerminal.API
{
    using Domain.CryptoExchanges.BinanceRealisation;
    using Domain.CryptoExchanges.Data;
    using Application.Interfaces;
    using Application.Users.Login;
    using Domain.Auth;
    using Domain.Database;
    using Infrastructure;
    using EFData;
    using Application.Users.IP;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDatabase(services);
            RegisterUserIdentity(services);

            // Make "Application" assembly - main handler of all queries.
            services.AddMediatR(typeof(LoginHandler).Assembly);

            AddJwtAuthentication(services);

            ConfigureIpCheck(services);

            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.AddControllers(opt =>
            {
                // Only authorized users
                opt.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));

                opt.Filters.Add(new AuthorizeFilter("SameIpPolicy"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CryptoTerminalContext context)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            BinanceCryptoExchange exchange = new BinanceCryptoExchange(
                "ZOcjoqRfQ86zSYz4vUyzQ4Hk63TilQGzMGskHp7d2Goc3TvCeoyHocuUo4EdAsp0",
                "iou3etuXmQYi7XWa666K7idpfNuvU3ucidwCvpWQ9v3FZURosrh62LFoRhJXVepk",
                context
                );

            exchange.GetFutures().First().MakeTWAPOrder(
                new TwapOrder(
                    "BTCUSDT",
                    0.2m,
                    0.02m,
                    DateTime.Now,
                    TimeSpan.FromMinutes(2),
                    Domain.CryptoExchanges.Enums.OrderSide.Buy,
                    Domain.CryptoExchanges.Enums.PositionSide.Long
                ));
            
        }

        private void RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<CryptoTerminalContext>(opt =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
                string connection = Configuration.GetConnectionString("DefaultConnection");
                opt.UseMySql(connection, serverVersion);
            });
        }

        private void RegisterUserIdentity(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<AppUser>();
            IdentityBuilder identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<CryptoTerminalContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
        }

        private void AddJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        private void ConfigureIpCheck(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, IpCheckHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SameIpPolicy",
                    policy => policy.Requirements.Add(new IpCheckRequirement { IpClaimRequired = true }));
            });
        }
    }
}
