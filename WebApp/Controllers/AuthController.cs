using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController : BaseController
{
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true) {
            return Redirect("/");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        Member? member = Provider.Member.Login(model);
        if (member == null)
        {
            ViewBag.Message = "Invalid email or password";
            return View(model);
        }
        List<Claim> claims = new(){
            new Claim(ClaimTypes.NameIdentifier, member.MemberId),
            new Claim(ClaimTypes.Name, member.Name),
            new Claim(ClaimTypes.Email, member.Email),
            new Claim(ClaimTypes.Role, member.Role.ToString())
        };
        ClaimsIdentity identity = new(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role
        );
        ClaimsPrincipal principal = new(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = model.Rem });
        return Redirect("/");
    }
    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) {
            return Redirect("/");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterModel model)
    {
        Provider.Member.Register(model);
        return Redirect("/");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/auth/login");
    }
}