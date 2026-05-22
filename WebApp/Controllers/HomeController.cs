using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index(short? brandId, int page = 1)
    {
        string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(memberId))
        {
            ViewBag.NumOfCart = Provider.Cart.Count(memberId);
        }
        int pageSize = 8;
        IEnumerable<Product> products;
        int totalProducts;
        if (brandId.HasValue)
        {
            products = Provider.Product.GetProductsByBrand(
                brandId.Value,
                page,
                pageSize
            );
            totalProducts = Provider.Product.CountProductsByBrand(
                brandId.Value
            );
        }
        else
        {
            products = Provider.Product.GetProducts(page, pageSize);
            totalProducts = Provider.Product.CountProducts();
        }
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
        ViewBag.SelectedBrandId = brandId;
        ViewBag.Categories = AbstractHelper.ToTree<short, Category, Brand>(Provider.Category.GetCategoriesWithIcon(), Provider.Brand.GetBrands());
        ViewBag.Shops = Provider.Brand.GetShops();
        return View(products);
    }
    public IActionResult Details(int id)
    {
        Product? product = Provider.Product.GetProductDetail(id);
        if(product == null)
        {
            return NotFound();
        }
        return View(product);
    }
}