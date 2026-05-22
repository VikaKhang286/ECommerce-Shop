namespace WebApp.Models;

public class Product
{
    public int ProductId { get; set; }
    public short BrandId { get; set; }
    public string BrandName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal SaleOff { get; set; }
    
}