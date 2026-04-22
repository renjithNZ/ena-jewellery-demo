using Microsoft.AspNetCore.Mvc;
using EnaStore.Models;
using EnaStore.Services;

namespace EnaStore.Controllers;

public class CheckoutController : Controller
{
    private readonly CartService _cartService;
    private readonly OrderService _orderService;

    public CheckoutController(CartService cartService, OrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }

    public IActionResult Index()
    {
        var items = _cartService.GetCart();
        if (!items.Any()) return RedirectToAction("Index", "Cart");

        var subtotal = items.Sum(c => c.LineTotal);
        var model = new CheckoutViewModel
        {
            Items = items,
            Subtotal = subtotal,
            ShippingCost = subtotal >= 250 ? 0m : 12m,
            Total = subtotal + (subtotal >= 250 ? 0m : 12m)
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(CheckoutViewModel model)
    {
        var items = _cartService.GetCart();
        model.Items = items;
        model.Subtotal = items.Sum(c => c.LineTotal);
        model.ShippingCost = model.ShippingMethod == "express" ? 20m : (model.Subtotal >= 250 ? 0m : 12m);
        model.Discount = model.CouponCode?.ToUpper() == "ENA10" ? model.Subtotal * 0.10m : 0m;
        model.Total = model.Subtotal + model.ShippingCost - model.Discount;

        if (!ModelState.IsValid) return View(model);

        // Simulated payment: card starting with 0000 is declined
        var cleanCard = model.CardNumber.Replace(" ", "").Replace("-", "");
        if (cleanCard.StartsWith("0000"))
        {
            ModelState.AddModelError("CardNumber", "Payment declined. Please use a different card. (Demo tip: use any card not starting with 0000)");
            return View(model);
        }

        var order = _orderService.PlaceOrder(model, items);
        _cartService.ClearCart();
        return RedirectToAction("Confirmation", new { orderId = order.OrderId });
    }

    public IActionResult Confirmation(string orderId)
    {
        var order = _orderService.GetOrder(orderId);
        if (order is null) return NotFound();
        return View(order);
    }
}
