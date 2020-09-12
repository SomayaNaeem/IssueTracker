using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Services.Identity.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<string> GetUserNameAsync(string userId);
	}
}
