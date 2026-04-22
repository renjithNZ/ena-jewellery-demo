using Microsoft.AspNetCore.Mvc;
using EnaStore.Services;

namespace EnaStore.Controllers;

public class AdminController : Controller
{
    private readonly ProductService _productService;
    private readonly OrderService _orderService;

    public AdminController(ProductService productService, OrderService orderService)
    {
        _productService = productService;
        _orderService = orderService;
    }

    public IActionResult Index()
    {
        var orders = _orderService.GetAllOrders();
        ViewBag.TotalOrders = orders.Count;
        ViewBag.TotalRevenue = orders.Sum(o => o.Total);
        ViewBag.TotalProducts = _productService.GetAll().Count;
        ViewBag.LowStockCount = _productService.GetAll().Count(p => p.StockCount < 6);
        ViewBag.RecentOrders = orders.Take(5).ToList();
        return View();
    }

    public IActionResult Products()
    {
        var products = _productService.GetAll();
        return View(products);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStock(int productId, int stock)
    {
        if (stock < 0) stock = 0;
        _productService.UpdateStock(productId, stock);
        TempData["Success"] = "Stock updated successfully.";
        return RedirectToAction("Products");
    }

    public IActionResult Orders()
    {
        var orders = _orderService.GetAllOrders();
        return View(orders);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateOrderStatus(string orderId, string status)
    {
        var allowed = new[] { "Placed", "Paid", "Processing", "Shipped", "Delivered", "Cancelled" };
        if (!allowed.Contains(status)) return BadRequest();
        _orderService.UpdateStatus(orderId, status);
        TempData["Success"] = $"Order {orderId} updated to {status}.";
        return RedirectToAction("Orders");
    }
}
