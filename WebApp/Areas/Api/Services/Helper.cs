using System.Security.Cryptography;
using System.Text;

namespace WebApp.Areas.Api.Services;
public static class Helper
{
    public static byte[] Hash(string plaintext)
    {
        HashAlgorithm algorithm = SHA512.Create();
        return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
    }
}