using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Infrastructure.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public IdentityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<string> GetUserNameAsync(string userId)
		{
			var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
			return user.UserName;
		}
	}
}
