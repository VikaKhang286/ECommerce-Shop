using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Api.Models;

namespace WebApp.Areas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    [Authorize]
    [HttpGet("details")]
    public Member? GetMember()
    {
        string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(id)) return null;
        return Provider.Member.GetMember(id);
    }
    
    [HttpPost("register")]
    public int Register(RegisterModel obj)
    {
        return Provider.Member.Save(obj);
    }
    
    [HttpPost("login")]
    public string? Login(LoginModel obj)
    {
        Member? member = Provider.Member.GetMember(obj);
        if (member is null) return null;
        List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, member.MemberId),
            new Claim(ClaimTypes.Name, member.Name),
            new Claim(ClaimTypes.GivenName, member.GivenName),
            new Claim(ClaimTypes.Email, member.Email),
            new Claim(ClaimTypes.Role, member.Role.ToString())
        };
        if (!string.IsNullOrEmpty(member.Surname)) claims.Add(new Claim(ClaimTypes.Surname, member.Surname));
        return null;
    }
}