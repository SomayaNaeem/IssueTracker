using IssueTracker.Services.Issues.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IssueTracker.Services.Issues.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
	{
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
        }

        public string UserId { get; }
    }
}
