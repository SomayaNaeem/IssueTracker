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

            optionsBuilder.UseSqlServer(@"Server=SOMAYA-IBRAHIM\SQLEXPRESS;Database=IssueTracker.Dev.DB.Issues;User Id=sa;Password=1234;MultipleActiveResultSets=true",
                 sqlServerOptionsAction: o => o.MigrationsAssembly("IssueTracker.Services.Issues.Infrastructure"));

            return new ApplicationDbContext(optionsBuilder.Options, _currentUserService, _dateTime);
        }
    }

}
