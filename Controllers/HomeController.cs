using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnaStore.Models;
using EnaStore.Services;

namespace EnaStore.Controllers;

public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.Featured = _productService.GetFeaturedProducts();
        ViewBag.Bestsellers = _productService.GetBestsellers();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
