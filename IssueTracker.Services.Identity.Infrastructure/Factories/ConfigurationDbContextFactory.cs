using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IssueTracker.Services.Identity.Infrastructure.Factories
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            var storeOptions = new ConfigurationStoreOptions();

            optionsBuilder.UseSqlServer(@"Data Source=SOMAYA-NAEEM\SQLEXPRESS;Initial Catalog=IssueTracker.Dev.DB.Identity;Persist Security Info=True;User ID=sa;Password=1234",
               sqlServerOptionsAction: o => o.MigrationsAssembly("IssueTracker.Services.Identity.Infrastructure"));

            return new ConfigurationDbContext(optionsBuilder.Options, storeOptions);
        }
    }

}
