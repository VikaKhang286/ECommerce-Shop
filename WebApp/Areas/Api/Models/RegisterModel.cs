namespace WebApp.Areas.Api.Models;

public class RegisterModel
{
    public string? MemberId { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string? Surname { get; set; }
    public Role Role { get; set; }
}