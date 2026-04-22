namespace EnaStore.Models;

public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public DateTime PlacedAt { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();
    public decimal Total { get; set; }
    public string Status { get; set; } = "Placed";
    public string PaymentStatus { get; set; } = "Paid";
    public string ShippingMethod { get; set; } = string.Empty;
}
