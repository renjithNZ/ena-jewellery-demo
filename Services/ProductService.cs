using EnaStore.Models;

namespace EnaStore.Services;

public class ProductService
{
    private readonly List<Product> _products;

    public ProductService()
    {
        _products = SeedProducts();
    }

    public List<Product> GetAll() => _products;

    public List<Product> GetByCategory(string category) =>
        _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Product> GetFeaturedProducts() =>
        _products.Where(p => p.IsFeatured).Take(8).ToList();

    public List<Product> GetBestsellers() =>
        _products.Where(p => p.IsBestseller).Take(4).ToList();

    public Product? GetBySlug(string slug) =>
        _products.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public void UpdateStock(int productId, int stock)
    {
        var product = GetById(productId);
        if (product != null)
        {
            product.StockCount = stock;
            product.InStock = stock > 0;
        }
    }

    private static List<Product> SeedProducts() => new()
    {
        // RINGS
        new Product
        {
            Id = 1, Name = "Solitaire Diamond Ring", Slug = "solitaire-diamond-ring",
            Category = "rings", Price = 299m,
            Description = "A timeless solitaire diamond ring set in 18k white gold. Features a brilliant-cut 0.25ct diamond with exceptional clarity.",
            ImageUrl = "/images/products/rings-a.svg",
            Material = "18k White Gold, Diamond", StockCount = 12, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 4.9, ReviewCount = 87,
            Sizes = new List<string> { "5", "6", "7", "8", "9" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 2, Name = "Gold Classic Band", Slug = "gold-classic-band",
            Category = "rings", Price = 149m,
            Description = "A sleek and classic 18k yellow gold band. Perfect as a wedding band or everyday wear. Polished finish.",
            ImageUrl = "/images/products/rings-b.svg",
            Material = "18k Yellow Gold", StockCount = 20, InStock = true,
            IsFeatured = true, Rating = 4.7, ReviewCount = 54,
            Sizes = new List<string> { "5", "6", "7", "8", "9", "10" },
            MetalColors = new List<string> { "Yellow Gold", "White Gold" }
        },
        new Product
        {
            Id = 3, Name = "Rose Gold Twisted Ring", Slug = "rose-gold-twisted-ring",
            Category = "rings", Price = 189m,
            Description = "A delicate twisted band in 14k rose gold. Modern design with a romantic touch. Comfort fit.",
            ImageUrl = "/images/products/rings-a.svg",
            Material = "14k Rose Gold", StockCount = 8, InStock = true,
            IsFeatured = false, IsBestseller = true, Rating = 4.8, ReviewCount = 43,
            Sizes = new List<string> { "5", "6", "7", "8" },
            MetalColors = new List<string> { "Rose Gold" }
        },
        new Product
        {
            Id = 4, Name = "Emerald Cocktail Ring", Slug = "emerald-cocktail-ring",
            Category = "rings", Price = 449m,
            Description = "A statement cocktail ring featuring a vivid emerald-cut gemstone surrounded by a halo of pavé diamonds.",
            ImageUrl = "/images/products/rings-b.svg",
            Material = "18k White Gold, Emerald, Diamond", StockCount = 5, InStock = true,
            IsFeatured = true, Rating = 4.6, ReviewCount = 29,
            Sizes = new List<string> { "6", "7", "8" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold" }
        },
        new Product
        {
            Id = 5, Name = "Pearl Promise Ring", Slug = "pearl-promise-ring",
            Category = "rings", Price = 129m,
            Description = "A delicate ring featuring a freshwater pearl centerpiece set in sterling silver. Pure and elegant.",
            ImageUrl = "/images/products/rings-a.svg",
            Material = "Sterling Silver, Freshwater Pearl", StockCount = 15, InStock = true,
            IsFeatured = false, Rating = 4.5, ReviewCount = 38,
            Sizes = new List<string> { "5", "6", "7", "8", "9" },
            MetalColors = new List<string> { "Silver", "Rose Gold" }
        },
        new Product
        {
            Id = 6, Name = "Sapphire Eternity Ring", Slug = "sapphire-eternity-ring",
            Category = "rings", Price = 599m,
            Description = "A full eternity band set with alternating sapphires and diamonds in 18k white gold. A symbol of endless love.",
            ImageUrl = "/images/products/rings-b.svg",
            Material = "18k White Gold, Sapphire, Diamond", StockCount = 4, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 5.0, ReviewCount = 21,
            Sizes = new List<string> { "5", "6", "7", "8" },
            MetalColors = new List<string> { "White Gold" }
        },

        // NECKLACES
        new Product
        {
            Id = 7, Name = "Diamond Solitaire Pendant", Slug = "diamond-solitaire-pendant",
            Category = "necklaces", Price = 349m,
            Description = "A brilliant-cut diamond pendant suspended on an 18k white gold cable chain. Timeless and versatile.",
            ImageUrl = "/images/products/necklaces-a.svg",
            Material = "18k White Gold, Diamond", StockCount = 10, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 4.9, ReviewCount = 102,
            Sizes = new List<string> { "16 inch", "18 inch", "20 inch" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 8, Name = "Gold Rope Chain", Slug = "gold-rope-chain",
            Category = "necklaces", Price = 99m,
            Description = "A classic 18k yellow gold rope chain. Durable, lustrous, and perfect to wear alone or layered.",
            ImageUrl = "/images/products/necklaces-b.svg",
            Material = "18k Yellow Gold", StockCount = 25, InStock = true,
            IsFeatured = false, Rating = 4.6, ReviewCount = 67,
            Sizes = new List<string> { "16 inch", "18 inch", "20 inch", "22 inch" },
            MetalColors = new List<string> { "Yellow Gold", "White Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 9, Name = "Pearl Strand Necklace", Slug = "pearl-strand-necklace",
            Category = "necklaces", Price = 199m,
            Description = "A classic strand of hand-knotted freshwater pearls with a secure 14k gold clasp. An heirloom piece.",
            ImageUrl = "/images/products/necklaces-a.svg",
            Material = "14k Gold, Freshwater Pearl", StockCount = 7, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 4.8, ReviewCount = 55,
            Sizes = new List<string> { "16 inch", "18 inch" },
            MetalColors = new List<string> { "Yellow Gold", "White Gold" }
        },
        new Product
        {
            Id = 10, Name = "Amethyst Drop Necklace", Slug = "amethyst-drop-necklace",
            Category = "necklaces", Price = 159m,
            Description = "A faceted amethyst teardrop pendant on a delicate 14k rose gold chain. Bold color, refined style.",
            ImageUrl = "/images/products/necklaces-b.svg",
            Material = "14k Rose Gold, Amethyst", StockCount = 9, InStock = true,
            IsFeatured = false, Rating = 4.7, ReviewCount = 34,
            Sizes = new List<string> { "16 inch", "18 inch" },
            MetalColors = new List<string> { "Rose Gold", "Yellow Gold" }
        },
        new Product
        {
            Id = 11, Name = "Gold Heart Locket", Slug = "gold-heart-locket",
            Category = "necklaces", Price = 179m,
            Description = "A classic heart-shaped locket in 14k yellow gold. Holds two miniature photos. Engraving available.",
            ImageUrl = "/images/products/necklaces-a.svg",
            Material = "14k Yellow Gold", StockCount = 11, InStock = true,
            IsFeatured = true, Rating = 4.8, ReviewCount = 78,
            Sizes = new List<string> { "18 inch", "20 inch" },
            MetalColors = new List<string> { "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 12, Name = "Infinity Diamond Chain", Slug = "infinity-diamond-chain",
            Category = "necklaces", Price = 279m,
            Description = "An elegant infinity symbol pendant adorned with pavé diamonds on an 18k white gold chain. Wear for eternity.",
            ImageUrl = "/images/products/necklaces-b.svg",
            Material = "18k White Gold, Diamond", StockCount = 6, InStock = true,
            IsFeatured = true, IsBestseller = false, Rating = 4.9, ReviewCount = 41,
            Sizes = new List<string> { "16 inch", "18 inch", "20 inch" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold" }
        },

        // EARRINGS
        new Product
        {
            Id = 13, Name = "Diamond Stud Earrings", Slug = "diamond-stud-earrings",
            Category = "earrings", Price = 249m,
            Description = "Brilliant-cut diamond studs in 18k white gold four-prong settings. A wardrobe essential.",
            ImageUrl = "/images/products/earrings-a.svg",
            Material = "18k White Gold, Diamond", StockCount = 14, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 5.0, ReviewCount = 134,
            MetalColors = new List<string> { "White Gold", "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 14, Name = "Pearl Drop Earrings", Slug = "pearl-drop-earrings",
            Category = "earrings", Price = 119m,
            Description = "Elegant freshwater pearl drops suspended from 14k gold hooks. Classic sophistication.",
            ImageUrl = "/images/products/earrings-b.svg",
            Material = "14k Gold, Freshwater Pearl", StockCount = 18, InStock = true,
            IsFeatured = false, IsBestseller = true, Rating = 4.7, ReviewCount = 63,
            MetalColors = new List<string> { "Yellow Gold", "White Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 15, Name = "Gold Hoop Earrings", Slug = "gold-hoop-earrings",
            Category = "earrings", Price = 89m,
            Description = "Classic thin hoops in 14k yellow gold. Lightweight and comfortable for all-day wear.",
            ImageUrl = "/images/products/earrings-a.svg",
            Material = "14k Yellow Gold", StockCount = 22, InStock = true,
            IsFeatured = true, Rating = 4.6, ReviewCount = 91,
            MetalColors = new List<string> { "Yellow Gold", "White Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 16, Name = "Crystal Chandelier Earrings", Slug = "crystal-chandelier-earrings",
            Category = "earrings", Price = 169m,
            Description = "Stunning chandelier earrings with cascading crystal drops set in 14k white gold. Perfect for special occasions.",
            ImageUrl = "/images/products/earrings-b.svg",
            Material = "14k White Gold, Crystal", StockCount = 6, InStock = true,
            IsFeatured = false, Rating = 4.8, ReviewCount = 27,
            MetalColors = new List<string> { "White Gold" }
        },
        new Product
        {
            Id = 17, Name = "Emerald Ear Cuffs", Slug = "emerald-ear-cuffs",
            Category = "earrings", Price = 209m,
            Description = "Contemporary ear cuffs featuring natural emerald stones in an 18k yellow gold setting. No piercing needed.",
            ImageUrl = "/images/products/earrings-a.svg",
            Material = "18k Yellow Gold, Emerald", StockCount = 8, InStock = true,
            IsFeatured = true, Rating = 4.7, ReviewCount = 19,
            MetalColors = new List<string> { "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 18, Name = "Rose Gold Teardrop Earrings", Slug = "rose-gold-teardrop-earrings",
            Category = "earrings", Price = 139m,
            Description = "Romantic teardrop earrings in 14k rose gold with a brushed satin finish. Light and graceful.",
            ImageUrl = "/images/products/earrings-b.svg",
            Material = "14k Rose Gold", StockCount = 16, InStock = true,
            IsFeatured = false, IsBestseller = true, Rating = 4.8, ReviewCount = 47,
            MetalColors = new List<string> { "Rose Gold" }
        },

        // BRACELETS
        new Product
        {
            Id = 19, Name = "Gold Tennis Bracelet", Slug = "gold-tennis-bracelet",
            Category = "bracelets", Price = 499m,
            Description = "A classic tennis bracelet set with 32 brilliant-cut diamonds in 18k white gold. Iconic and dazzling.",
            ImageUrl = "/images/products/bracelets-a.svg",
            Material = "18k White Gold, Diamond", StockCount = 4, InStock = true,
            IsFeatured = true, IsBestseller = true, Rating = 5.0, ReviewCount = 56,
            Sizes = new List<string> { "6.5 inch", "7 inch", "7.5 inch" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold" }
        },
        new Product
        {
            Id = 20, Name = "Pearl Charm Bracelet", Slug = "pearl-charm-bracelet",
            Category = "bracelets", Price = 149m,
            Description = "A delicate 14k gold chain bracelet adorned with three freshwater pearl charms. Elegant and playful.",
            ImageUrl = "/images/products/bracelets-b.svg",
            Material = "14k Yellow Gold, Freshwater Pearl", StockCount = 13, InStock = true,
            IsFeatured = false, Rating = 4.7, ReviewCount = 42,
            Sizes = new List<string> { "6.5 inch", "7 inch", "7.5 inch" },
            MetalColors = new List<string> { "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 21, Name = "Diamond Bangle", Slug = "diamond-bangle",
            Category = "bracelets", Price = 379m,
            Description = "A rigid bangle in 18k white gold set with a single row of pavé diamonds on top. Understated luxury.",
            ImageUrl = "/images/products/bracelets-a.svg",
            Material = "18k White Gold, Diamond", StockCount = 6, InStock = true,
            IsFeatured = true, IsBestseller = false, Rating = 4.9, ReviewCount = 31,
            Sizes = new List<string> { "Small (2.4\")", "Medium (2.6\")", "Large (2.8\")" },
            MetalColors = new List<string> { "White Gold", "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 22, Name = "Blue Topaz Chain Bracelet", Slug = "blue-topaz-chain-bracelet",
            Category = "bracelets", Price = 189m,
            Description = "A sterling silver chain bracelet featuring a luminous blue topaz station every few links. Fresh and vibrant.",
            ImageUrl = "/images/products/bracelets-b.svg",
            Material = "Sterling Silver, Blue Topaz", StockCount = 9, InStock = true,
            IsFeatured = false, Rating = 4.6, ReviewCount = 23,
            Sizes = new List<string> { "6.5 inch", "7 inch", "7.5 inch" },
            MetalColors = new List<string> { "Silver" }
        },
        new Product
        {
            Id = 23, Name = "Delicate Gold Cuff", Slug = "delicate-gold-cuff",
            Category = "bracelets", Price = 129m,
            Description = "A slim open cuff in 14k yellow gold with a subtle hammered texture. Minimalist and modern.",
            ImageUrl = "/images/products/bracelets-a.svg",
            Material = "14k Yellow Gold", StockCount = 11, InStock = true,
            IsFeatured = true, Rating = 4.7, ReviewCount = 38,
            MetalColors = new List<string> { "Yellow Gold", "Rose Gold" }
        },
        new Product
        {
            Id = 24, Name = "Silver Infinity Bracelet", Slug = "silver-infinity-bracelet",
            Category = "bracelets", Price = 99m,
            Description = "A dainty sterling silver bracelet with an infinity charm. Adjustable chain length, perfect gifting choice.",
            ImageUrl = "/images/products/bracelets-b.svg",
            Material = "Sterling Silver", StockCount = 30, InStock = true,
            IsFeatured = false, IsBestseller = true, Rating = 4.5, ReviewCount = 116,
            Sizes = new List<string> { "Adjustable" },
            MetalColors = new List<string> { "Silver" }
        }
    };
}
