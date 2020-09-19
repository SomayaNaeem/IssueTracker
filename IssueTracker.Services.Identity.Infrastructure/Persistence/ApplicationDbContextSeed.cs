using IssueTracker.Services.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Seed, if necessary

            // Create default administrator
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }
    }
}
