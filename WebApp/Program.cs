using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Models;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SiteProvider>();
builder.Services.AddControllers();

builder.Services
    .AddAuthentication(p =>
    {
        p.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        p.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        p.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(p =>
    {
        p.LoginPath = "/auth/login";
        p.LogoutPath = "/auth/logout";
        p.AccessDeniedPath = "/auth/denied";
    });
builder.Services.AddAuthorization();
builder.Services.AddSignalR();
builder.Services.AddMvc();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapHub<NotifyHub>("/notify");

// app.MapControllers();
app.MapControllerRoute(name: "dashboard", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");

app.Run();