using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string FullName { get; }
        string Email { get; }
        string Token { get; }
    }
}
