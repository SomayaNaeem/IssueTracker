using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Services.Issues.Application.Participant;

namespace IssueTracker.Services.Issues.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IssuesConnectionString"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddScoped<IParticipantService>(provider => provider.GetService<ParticipantService>());
            return services;
        }
    }
}
