using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Serilog;

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
    using Additional;
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
            services.AddDbContext<CryptoTerminalContext>(opt =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
                string connection = Configuration.GetConnectionString("DefaultConnection");
                opt.UseMySql(connection, serverVersion);
            });
            IdentityBuilder builder = services.AddIdentityCore<AppUser>();
            IdentityBuilder identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddEntityFrameworkStores<CryptoTerminalContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CryptoTerminal API",
                    Description = "An ASP.NET Core Web API for managing CryptoTerminal items",
                });

                string xmlFilename = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.AddSignalRSwaggerGen(opt =>
                {
                    opt.AutoDiscover = SignalRSwaggerGen.Enums.AutoDiscover.MethodsAndParams;
                    opt.UseXmlComments(xmlFilename);
                });
                options.IncludeXmlComments(xmlFilename);
                options.OperationFilter<FormatXmlCommentProperties>();
            });

            // Make "Application" assembly - main handler of all queries.
            services.AddMediatR(typeof(LoginHandler).Assembly);

            services.AddSignalR();

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

            // Add user accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //! these lines are for configuring authorization
            //! use ONLY this line (ip check) (also requires line inside AddControllers block)
            //ConfigureIpCheck(services);
            //! OR THIS (basic)
            services.AddAuthorization();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<RealtimeSpotDepthMarket>();

            IMvcBuilder mvcBuilder = services.AddControllers(opt =>
            {
                // Only authorized users
                opt.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                //opt.Filters.Add(new AuthorizeFilter("SameIpPolicy"));
            });

            mvcBuilder.AddNewtonsoftJson();
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            mvcBuilder.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssembly(typeof(AddExchangeTokenCommandValidator).Assembly));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
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
                endpoints.MapHub<BinanceSpotDepthMarketHub>("/api/binance/spot/realtime/depth-market");
            });

            Log.Information("Server is started");
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
