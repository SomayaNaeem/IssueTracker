using IssueTracker.Services.Identity.Application.Common.Models;
using IssueTracker.Services.Identity.Application.SignUp.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<string> GetUserNameAsync(string userId);
		Task<(Result Result, string id)> CreateUserAsync(SignUpCommand requesterSignUp);

	}
}
