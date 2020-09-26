using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using IssueTracker.Services.Identity.Application.Common.Exceptions;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Domain.Entities;
using IssueTracker.Services.Identity.Infrastructure.Configuration;
using IssueTracker.Services.Identity.Infrastructure.Identity;
using IssueTracker.Services.Identity.Infrastructure.Persistence;
using IssueTracker.Services.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IssueTracker.Services.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment, string migrationsAssembly)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.User.RequireUniqueEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<ApplicationUser>)));
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.AllowedForNewUsers = true;
            })
          .AddEntityFrameworkStores<ApplicationDbContext>().AddErrorDescriber<CustomIdentityErrorDescriber>().AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LogoutUrl = "/account/logout",
                    LoginUrl = "/account/login",
                    LoginReturnUrlParameter = "returnUrl"
                };
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddDeveloperSigningCredential()
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                                sql => sql.MigrationsAssembly(migrationsAssembly));
              })
              .AddInMemoryIdentityResources(Config.GetResources())
          .AddInMemoryApiResources(Config.GetApis())
          .AddInMemoryClients(Config.GetClients(configuration))
          .AddInMemoryApiScopes(Config.GetApiScopes())
     .AddOperationalStore(options =>
     {
         options.ConfigureDbContext = b =>
             b.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                 sql => sql.MigrationsAssembly(migrationsAssembly));

         // this enables automatic token cleanup. this is optional.
         options.EnableTokenCleanup = true;
     })
     .AddAspNetIdentity<ApplicationUser>();

            services.AddHttpContextAccessor();
            services.AddMvcCore().AddAuthorization()
            .AddNewtonsoftJson();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddOAuth2Introspection(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                 {
                     options.Authority = configuration.GetSection("Identity:IdentityAuthUrl").Value;
                     // this maps to the API resource name and secret
                     options.ClientId = configuration.GetSection("Identity:APIName").Value;
                     options.ClientSecret = configuration.GetSection("Identity:API_secret").Value;
                 });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            return services;
        }

    }

}
