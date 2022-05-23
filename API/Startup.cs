using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using MediatR;

namespace Ixcent.CryptoTerminal.Api
{
    using Application.Exchanges.Binance;
    using Application.Users.Login;
    using Application.Interfaces;
    using Application.Validation;
    using Application.Users.IP;
    using Domain.Database;
    using Infrastructure;
    using Domain.Auth;
    using EFData;
    using Hubs;

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

            services.AddSignalR();

            AddJwtAuthentication(services);

            // Add user accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //! these lines are for configuring authorization
            //! use ONLY this line (ip check) (also requires line inside AddControllers block)
            //ConfigureIpCheck(services);
            //! OR THIS (basic)
            services.AddAuthorization();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<ExchangesValidator>();
            services.AddSingleton<RealtimeSpotDepthMarket>();

            services.AddControllers(opt =>
            {
                // Only authorized users
                opt.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));

                //opt.Filters.Add(new AuthorizeFilter("SameIpPolicy"));
            }).AddNewtonsoftJson()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(AddExchangeTokenCommandValidator).Assembly));
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

            app.UseMiddleware<Middlewares.ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<BinanceSpotDepthMarketHub>("/api/realtime/depth-market");
            });
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
            identityBuilder.AddRoles<IdentityRole>();
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
                        ValidateIssuer = false,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = false,
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
