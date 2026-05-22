using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class CartController : BaseController
{
    [Authorize]
    public IActionResult Index()
    {
        string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var carts = Provider.Cart.GetCarts(memberId!);
        return View(carts);
    }

    [HttpPost]
    public IActionResult Save( int productId, short quantity = 1)
    {
        try {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // return Content(memberId);
            Provider.Cart.Save(memberId!, productId, quantity);
            return Content("OK");
        }
        catch(Exception ex)
        {
            return Content(ex.ToString());
        }
    }
    
    public IActionResult Delete(Guid id)
    {
        Provider.Cart.Delete(id);
        return Redirect("/cart");
    }

    [HttpPost]
    public IActionResult Update(Guid cartId, short quantity)
    {
        Provider.Cart.UpdateQuantity(cartId, quantity);
        return Redirect("/cart");
    }
}