namespace EnaStore.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public bool InStock { get; set; } = true;
    public int StockCount { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsBestseller { get; set; }
    public List<string> Sizes { get; set; } = new();
    public List<string> MetalColors { get; set; } = new();
    public string CareInfo { get; set; } = "Store in a cool, dry place. Avoid contact with perfumes and chemicals.";
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
}
