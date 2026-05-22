namespace WebApp.Models;

public class Cart
{
    public Guid CartId { get; set; }
    public string MemberId { get; set; } = null!;
    public int ProductId { get; set; }
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal SaleOff { get; set; }
    public short Quantity { get; set; }
}