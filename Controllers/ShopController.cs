using Microsoft.AspNetCore.Mvc;
using EnaStore.Services;

namespace EnaStore.Controllers;

public class ShopController : Controller
{
    private readonly ProductService _productService;

    public ShopController(ProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index(string? category, string? sort, decimal? minPrice, decimal? maxPrice)
    {
        var products = _productService.GetAll();

        if (!string.IsNullOrEmpty(category) && category != "all")
            products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        if (minPrice.HasValue) products = products.Where(p => p.Price >= minPrice.Value).ToList();
        if (maxPrice.HasValue) products = products.Where(p => p.Price <= maxPrice.Value).ToList();

        products = sort switch
        {
            "price-asc" => products.OrderBy(p => p.Price).ToList(),
            "price-desc" => products.OrderByDescending(p => p.Price).ToList(),
            "rating" => products.OrderByDescending(p => p.Rating).ToList(),
            _ => products.OrderByDescending(p => p.IsFeatured).ThenByDescending(p => p.ReviewCount).ToList()
        };

        ViewBag.CurrentCategory = category ?? "all";
        ViewBag.CurrentSort = sort ?? "";
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.Categories = new[] { "all", "rings", "necklaces", "earrings", "bracelets" };
        ViewBag.TotalCount = products.Count;

        return View(products);
    }

    public IActionResult Details(string slug)
    {
        var product = _productService.GetBySlug(slug);
        if (product is null) return NotFound();

        var related = _productService.GetByCategory(product.Category)
            .Where(p => p.Id != product.Id)
            .Take(4)
            .ToList();

        ViewBag.Related = related;
        return View(product);
    }
}
