namespace WebApp.Models;

public class Brand : Child<short>
{
    public short BrandId { get; set; }
    // public short CategoryId { get; set; }
    public string BrandName { get; set; } = null!;
    public short ItemCount { get; set; }
    public string? Logo { get; set; }
}