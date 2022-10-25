using System.Reflection;

using AutoMapper;

using FluentValidation.AspNetCore;

using Ixcent.CryptoTerminal.Api.Additional;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Services;
using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Users;
using Ixcent.CryptoTerminal.Application.Users.Login;
using Ixcent.CryptoTerminal.Application.Users.Registration;
using Ixcent.CryptoTerminal.Application.Users.Services;
using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Auth;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Infrastructure;
using Ixcent.CryptoTerminal.StorageHandle;
using Ixcent.CryptoTerminal.StorageHandle.ExchangeTokens;
using Ixcent.CryptoTerminal.StorageHandle.UserRepository;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;

namespace Ixcent.CryptoTerminal.Api
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
            services.AddDbContext<CryptoTerminalContext>(opt =>
            {
                MySqlServerVersion? serverVersion = new(new Version(8, 0, 27));
                string connection = Configuration.GetConnectionString("DefaultConnection");
                opt.UseMySql(connection, serverVersion);
            });
            IdentityBuilder builder = services.AddIdentityCore<AppUser>();
            IdentityBuilder identityBuilder = new(builder.UserType, builder.Services);
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
                options.CustomSchemaIds(type => type.ToString());
            });
            
            // Users service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserManagerRepository>();
            // Exchanges services
            services.AddScoped<ExchangesValidatorByToken>();
            services.AddScoped<IExchangeTokenService, ExchangeTokensService>();
            services.AddScoped<IExchangeTokenRepository, EntityFrameworkExchangeTokensRepository>();

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

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            StringValues accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            // Add user accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();;
            // Auto mapper
            MapperConfiguration? mapperConfig = new(mc =>
            {
                // Possible to add profiles here
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            //! these lines are for configuring authorization
            //! use ONLY this line (ip check) (also requires line inside AddControllers block)
            //ConfigureIpCheck(services);
            //! OR THIS (basic)
            services.AddAuthorization();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<DepthMarket>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            });

            IMvcBuilder mvcBuilder = services.AddControllers(opt =>
            {
                // Only authorized users
                opt.EnableEndpointRouting = false;
                AuthorizationPolicy? policy = new AuthorizationPolicyBuilder()
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
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseMiddleware<Middlewares.ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapSignalRHubs();
            });

            Log.Information("Server is started");
        }

        //private void ConfigureIpCheck(IServiceCollection services)
        //{
        //    services.AddSingleton<IAuthorizationHandler, IpCheckHandler>();
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("SameIpPolicy",
        //            policy => policy.Requirements.Add(new IpCheckRequirement { IpClaimRequired = true }));
        //    });
        //}
    }
}
