using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebApp.Services;

public static class Helper
{
    public static int GetPage(this ViewContext context, string name){
        object? obj = context.RouteData.Values[name];
        if(obj is null) return 1; 
        return Convert.ToInt32(obj);
    }
    public static string Slug(this string text) {
        return WebUtility.UrlEncode(text).ToLower();
    }
    public static byte[] Hash(string plaintext)
    {
        HashAlgorithm algorithm = SHA512.Create();
        return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
    }
    public static bool IsValidImage(IFormFile file)
    {
        if (file == null || file.Length == 0){
            return false;
        }
        string[] extensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
        string ext = Path.GetExtension(file.FileName).ToLower();
        if (!extensions.Contains(ext))
        {
            return false;
        }
        string[] contentTypes = ["image/jpeg", "image/png", "image/webp", "image/gif"];
        if (!contentTypes.Contains(file.ContentType.ToLower()))
        {
            return false;
        }
        return true;
    }
}