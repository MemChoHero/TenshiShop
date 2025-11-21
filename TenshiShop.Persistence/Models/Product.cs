using TenshiShop.Persistence.Traits;

namespace TenshiShop.Persistence.Models;

public class Product : IHasTimestamps
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string SlugSource => Name;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Category> Categories { get; set; } = new List<Category>();
}