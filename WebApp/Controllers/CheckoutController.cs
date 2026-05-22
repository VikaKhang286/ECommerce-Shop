using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class CheckoutController : BaseController
{
    public IActionResult Index()
    {
        string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var carts = Provider.Cart.GetCarts(memberId!);
        return View(carts);
    }
}