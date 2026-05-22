
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Areas.Dashboard.Controllers;

[Area("dashboard"), Authorize(Roles = "Admin")]
public class HomeController : BaseController
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Members = await Provider.Member.GetMembers();
        return View();
    }
}