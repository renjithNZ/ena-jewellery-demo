using Microsoft.AspNetCore.Mvc;
using EnaStore.Services;

namespace EnaStore.Controllers;

public class CartController : Controller
{
    private readonly CartService _cartService;
    private readonly ProductService _productService;

    public CartController(CartService cartService, ProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        ViewBag.Subtotal = cart.Sum(c => c.LineTotal);
        ViewBag.ShippingCost = cart.Sum(c => c.LineTotal) >= 250 ? 0m : 12m;
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int productId, int quantity, string? size, string? metal)
    {
        var product = _productService.GetById(productId);
        if (product is null) return NotFound();

        _cartService.AddToCart(product, quantity < 1 ? 1 : quantity, size, metal);
        TempData["AddedToCart"] = product.Name;
        return RedirectToAction("Details", "Shop", new { slug = product.Slug });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int productId, string? size, string? metal)
    {
        _cartService.RemoveFromCart(productId, size, metal);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int productId, string? size, string? metal, int quantity)
    {
        _cartService.UpdateQuantity(productId, size, metal, quantity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _cartService.ClearCart();
        return RedirectToAction("Index");
    }
}
