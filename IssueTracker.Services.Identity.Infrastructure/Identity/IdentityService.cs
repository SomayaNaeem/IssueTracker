using IssueTracker.Services.Identity.Application.Common.Extensions;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Application.Common.Models;
using IssueTracker.Services.Identity.Application.SignUp.Commands;
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

		public async Task<(Result Result, string id)> CreateUserAsync(SignUpCommand userSignUp)
		{
			var user = new ApplicationUser
			{
				Email = userSignUp.Email,	
				UserName=userSignUp.Email,
				Name = userSignUp.Name,				
			};
			var result = await _userManager.CreateAsync(user, userSignUp.Password);
			string userId = null;
			if (result.Succeeded)
			{
				userId = user.Id;
			}
			return (result.ToApplicationResult(), userId);
		}

		public async Task<ApplicationUser> GetUser(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}

		public async Task<string> GetUserNameAsync(string userId)
		{
			var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
			return user.UserName;
		}
	}
}
