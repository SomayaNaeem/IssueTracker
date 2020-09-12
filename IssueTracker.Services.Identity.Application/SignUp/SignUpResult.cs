using IssueTracker.Services.Identity.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.SignUp
{
    public class SignUpResult : Result
    {
        public SignUpResult(bool success, IEnumerable<string> errors, string id) : base(success, errors)
        {
            UserId = id;
        }
        public string UserId { get; set; }
    }
}
