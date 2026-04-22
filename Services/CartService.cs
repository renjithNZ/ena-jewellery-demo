using EnaStore.Models;
using System.Text.Json;

namespace EnaStore.Services;

public class CartService
{
    private const string CartSessionKey = "EnaCart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public List<CartItem> GetCart()
    {
        var json = Session.GetString(CartSessionKey);
        return json is null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json)!;
    }

    private void SaveCart(List<CartItem> cart) =>
        Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));

    public void AddToCart(Product product, int quantity, string? size, string? metal)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(c =>
            c.ProductId == product.Id &&
            c.SelectedSize == size &&
            c.SelectedMetal == metal);

        if (existing is not null)
            existing.Quantity += quantity;
        else
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = quantity,
                SelectedSize = size,
                SelectedMetal = metal
            });

        SaveCart(cart);
    }

    public void RemoveFromCart(int productId, string? size, string? metal)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c =>
            c.ProductId == productId && c.SelectedSize == size && c.SelectedMetal == metal);
        if (item is not null) cart.Remove(item);
        SaveCart(cart);
    }

    public void UpdateQuantity(int productId, string? size, string? metal, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c =>
            c.ProductId == productId && c.SelectedSize == size && c.SelectedMetal == metal);
        if (item is null) return;
        if (quantity <= 0) cart.Remove(item);
        else item.Quantity = quantity;
        SaveCart(cart);
    }

    public void ClearCart() => Session.Remove(CartSessionKey);

    public int GetCartCount() => GetCart().Sum(c => c.Quantity);

    public decimal GetCartTotal() => GetCart().Sum(c => c.LineTotal);
}
