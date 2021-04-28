using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;
using System.Linq;

public class AccountController : Controller
{
    public IActionResult SignIn()
    {
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            return Challenge(OktaDefaults.MvcAuthenticationScheme);
        }
        switch (User.Claims.First(c => c.Type == "usrtype").Value)
        {
            case ("0"):
                return new RedirectToPageResult("/Tracing/Dashboard");
            case ("1"):
                return new RedirectToPageResult("/Testing/Dashboard");
            case "2":
                return new RedirectToPageResult("/GovAgent/GovHome");

        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult SignOut()
    {
        return new SignOutResult(
            new[]
            {
                OktaDefaults.MvcAuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme,
         },
            new AuthenticationProperties { RedirectUri = "/" }); // redirected place after sign out
    }
}
