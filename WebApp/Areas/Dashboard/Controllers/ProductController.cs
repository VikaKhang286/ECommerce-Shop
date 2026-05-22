using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Dashboard.Controllers;

[Area("dashboard")]
[Authorize(Roles = "Admin")]
public class ProductController : BaseController
{
    private IWebHostEnvironment env;
    public ProductController(IWebHostEnvironment env)
    {
        this.env = env;
    }
    public IActionResult Index(
        string? keyword,
        string? sort,
        int page = 1
    )
    {
        int pageSize = 10;

        int totalProducts;

        var products = Provider.Product.SearchProducts(
            keyword,
            sort,
            page,
            pageSize,
            out totalProducts
        );

        ViewBag.Keyword = keyword;
        ViewBag.Sort = sort;

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages =
            (int)Math.Ceiling((double)totalProducts / pageSize);

        return View(products);
    }

    public IActionResult Create()
{
    ViewBag.Brands = Provider.Brand.GetBrands();
    return View();
}

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile? imageFile)
    {
        if (imageFile == null || !Helper.IsValidImage(imageFile))
        {
            ViewBag.Message = "Invalid image file";
            ViewBag.Brands = Provider.Brand.GetBrands();
            return View(model);
        }
        string folder = Path.Combine(env.WebRootPath, "images/products");
        string ext = Path.GetExtension(imageFile.FileName);
        string name = Path.GetFileNameWithoutExtension(imageFile.FileName);
        name = name.Replace(" ", "-");
        string fileName = name + ext;
        string path = Path.Combine(folder, fileName);
        using FileStream stream = new(path, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        model.ImageUrl = fileName;
        Provider.Product.Insert(model);
        return Redirect("/dashboard/product");
    }

    public IActionResult Edit(int id)
    {
        ViewBag.Brands = Provider.Brand.GetBrands();
        var product = Provider.Product.GetById(id);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product model, IFormFile? imageFile){
        if (imageFile != null && !Helper.IsValidImage(imageFile))
        {
            ViewBag.Message = "Invalid image file";
            return View(model);
        }
        if(imageFile != null){
            if (!string.IsNullOrEmpty(model.ImageUrl)){
                string oldPath = Path.Combine(env.WebRootPath, model.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath)){
                    System.IO.File.Delete(oldPath);
                }
            }
            string folder = Path.Combine(env.WebRootPath, "images/products");
            string ext = Path.GetExtension(imageFile.FileName);
            string name = Path.GetFileNameWithoutExtension(imageFile.FileName);
            name = name.Replace(" ", "-");
            string fileName = name + ext;
            string path = Path.Combine(folder, fileName);
            using FileStream stream = new(path, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            model.ImageUrl = fileName;
        }
        Provider.Product.Update(model);
        return Redirect("/dashboard/product");
    }

    public IActionResult Delete(int id){
        Product? product = Provider.Product.GetById(id);
        if (product != null && !string.IsNullOrEmpty(product.ImageUrl)){
            string path = Path.Combine(env.WebRootPath, product.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(path)){
                System.IO.File.Delete(path);
            }
        }
        Provider.Product.Delete(id);
        return Redirect("/dashboard/product");
    }
}