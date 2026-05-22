using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Api.Models;
namespace WebApp.Areas.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController
{
    public object GetRoles()
    {
        return Enum.GetValues<Role>().Select(p => new
        {
            Id = p,
            Name = p.ToString()
        });
    }
}