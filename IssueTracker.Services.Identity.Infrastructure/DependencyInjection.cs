﻿using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using IssueTracker.Services.Identity.Application.Common.Exceptions;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Domain.Entities;
using IssueTracker.Services.Identity.Infrastructure.Configuration;
using IssueTracker.Services.Identity.Infrastructure.Identity;
using IssueTracker.Services.Identity.Infrastructure.Persistence;
using IssueTracker.Services.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
     .AddOperationalStore(options =>
     {
         options.ConfigureDbContext = b =>
             b.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                 sql => sql.MigrationsAssembly(migrationsAssembly));

         // this enables automatic token cleanup. this is optional.
         options.EnableTokenCleanup = true;
     })
     .AddAspNetIdentity<ApplicationUser>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = configuration.GetSection("Identity:IdentityAuthUrl").Value;
               options.RequireHttpsMetadata = false;
               options.ApiName = configuration.GetSection("Identity:APIName").Value;
           });
           
            return services;
        }

    }

}