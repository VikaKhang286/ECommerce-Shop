namespace WebApp.Areas.Api.Models;

public class Member
{
    public string MemberId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string Name => (string.IsNullOrEmpty(Surname) ? "" : Surname + " ") + GivenName;
    public string? Surname { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? RegisterDate { get; set; }
    public Role Role { get; set; }
}