using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Services.Identity.Application.Profile.Queries;
using IssueTracker.Services.Identity.Application.SignUp;
using IssueTracker.Services.Identity.Application.SignUp.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Services.Identity.WebUI.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost("SignUp")]
        public async Task<SignUpResult> UserSignUp(SignUpCommand command)
        {
            return await Mediator.Send(command);
        }
        [AllowAnonymous]
        [HttpGet("Profile/{email}")]
        public async Task<ProfileInfoDto> ViewProfile(string email)
        {
            return await Mediator.Send(new GetProfileInfoQuery() {  Email=email});
        }
    }
}