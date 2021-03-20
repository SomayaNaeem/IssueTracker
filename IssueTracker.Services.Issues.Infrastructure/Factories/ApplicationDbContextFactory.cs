using IssueTracker.Services.Issues.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public ApplicationDbContextFactory()
        {

        }
        public ApplicationDbContextFactory(ICurrentUserService currentUserService,
            IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(@"Server=SOMAYA-NAEEM\\SQLEXPRESS;Database=IssueTracker.Dev.DB.Issues;MultipleActiveResultSets=true",
                 sqlServerOptionsAction: o => o.MigrationsAssembly("IssueTracker.Services.Issues.Infrastructure"));

            return new ApplicationDbContext(optionsBuilder.Options, _currentUserService, _dateTime);
        }
    }

}
