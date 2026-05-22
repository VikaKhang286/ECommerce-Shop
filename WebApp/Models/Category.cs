namespace WebApp.Models;

public class Category : Parent<short>
{
    // public short CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Icon { get; set; }
    // public IEnumerable<Brand>? SubCategories { get; set; }
}