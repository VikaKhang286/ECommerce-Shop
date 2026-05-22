using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Areas.Dashboard.Controllers;

[Area("dashboard")]
[Authorize(Roles = "Admin")]
public class RoleController : BaseController
{
    public async Task<IActionResult> Index(){ 
        return View(await Provider.Role.GetRolesAsync());
    }
}