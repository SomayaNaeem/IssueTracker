using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IssueTracker.Services.Identity.Infrastructure.Factories
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


            optionsBuilder.UseSqlServer(@"Data Source=SOMAYA-NAEEM\SQLEXPRESS;Initial Catalog=IssueTracker.Dev.DB.Identity;Persist Security Info=True;User ID=sa;Password=1234",
                sqlServerOptionsAction: o => o.MigrationsAssembly("IssueTracker.Services.Identity.Infrastructure"));


            return new ApplicationDbContext(optionsBuilder.Options, _currentUserService, _dateTime);
        }
    }

}
