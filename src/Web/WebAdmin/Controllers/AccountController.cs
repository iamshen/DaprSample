using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    public IActionResult Logout() => new SignOutResult(new List<string>
        { CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme });
}
