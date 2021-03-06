﻿using IssueTracker.Services.Issues.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace IssueTracker.Services.Issues.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
	{
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
            FullName = httpContextAccessor.HttpContext?.User?.FindFirstValue("FullName");
            Email= httpContextAccessor.HttpContext?.User?.FindFirstValue("email");            

        }

        public string UserId { get; }
        public string FullName { get; }
        public string Email { get;}
    }
}
