using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public abstract class BaseController : Controller{
    SiteProvider? provider;
    protected SiteProvider Provider => provider ??= HttpContext.RequestServices.GetRequiredService<SiteProvider>();
}