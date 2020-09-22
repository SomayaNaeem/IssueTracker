using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.WebClient.Controllers
{
    public class AccountController : Controller
    {
       

        [Authorize]
        public async Task<IActionResult> SignIn()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            ViewBag.Token = accessToken;
            return View();
        }
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            var homeUrl = Url.Action(nameof(HomeController.Index));
            return new SignOutResult("oidc",
                new AuthenticationProperties { RedirectUri = homeUrl });
        }
    }
}