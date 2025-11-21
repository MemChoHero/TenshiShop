using TenshiShop.Persistence.Traits;

namespace TenshiShop.Persistence.Models;

public class Category : IHasTimestamps
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string SlugSource => Name;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
}