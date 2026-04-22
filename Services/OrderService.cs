using EnaStore.Models;

namespace EnaStore.Services;

public class OrderService
{
    private static readonly List<Order> _orders = new();

    public Order PlaceOrder(CheckoutViewModel checkout, List<CartItem> items)
    {
        var shippingCost = checkout.ShippingMethod == "express" ? 20m : (checkout.Subtotal >= 250 ? 0m : 12m);
        var discount = checkout.CouponCode?.ToUpper() == "ENA10" ? checkout.Subtotal * 0.10m : 0m;
        var total = checkout.Subtotal + shippingCost - discount;

        var order = new Order
        {
            OrderId = $"ENA-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}",
            PlacedAt = DateTime.UtcNow,
            CustomerName = $"{checkout.FirstName} {checkout.LastName}",
            CustomerEmail = checkout.Email,
            ShippingAddress = $"{checkout.Address}, {checkout.City}, {checkout.PostalCode}, {checkout.Country}",
            Items = items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ImageUrl = i.ImageUrl,
                Price = i.Price,
                Quantity = i.Quantity,
                SelectedSize = i.SelectedSize,
                SelectedMetal = i.SelectedMetal
            }).ToList(),
            Total = total,
            Status = "Placed",
            PaymentStatus = "Paid",
            ShippingMethod = checkout.ShippingMethod
        };

        _orders.Insert(0, order);
        return order;
    }

    public List<Order> GetAllOrders() => _orders;

    public Order? GetOrder(string orderId) =>
        _orders.FirstOrDefault(o => o.OrderId == orderId);

    public void UpdateStatus(string orderId, string status)
    {
        var order = GetOrder(orderId);
        if (order is not null) order.Status = status;
    }

    public void SeedDemoOrders(List<Product> products)
    {
        if (_orders.Count > 0) return;

        var names = new[] { "Priya Sharma", "Anika Mehta", "Sarah Johnson", "Emma Wilson", "Nina Patel", "Rachel Green", "Meera Nair", "Zara Khan" };
        var emails = new[] { "priya@demo.com", "anika@demo.com", "sarah@demo.com", "emma@demo.com", "nina@demo.com", "rachel@demo.com", "meera@demo.com", "zara@demo.com" };
        var statuses = new[] { "Placed", "Paid", "Shipped", "Delivered" };
        var rng = new Random(42);

        for (int i = 0; i < 8; i++)
        {
            var product = products[rng.Next(products.Count)];
            _orders.Add(new Order
            {
                OrderId = $"ENA-202604{15 + i}-{1001 + i}",
                PlacedAt = DateTime.UtcNow.AddDays(-(7 - i)),
                CustomerName = names[i],
                CustomerEmail = emails[i],
                ShippingAddress = "18 Collins Street, Melbourne VIC 3000, Australia",
                Items = new List<CartItem>
                {
                    new()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Quantity = 1
                    }
                },
                Total = product.Price + (i % 3 == 0 ? 12m : 0m),
                Status = statuses[i % 4],
                PaymentStatus = "Paid",
                ShippingMethod = i % 2 == 0 ? "standard" : "express"
            });
        }
    }
}
