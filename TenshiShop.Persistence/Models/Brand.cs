using TenshiShop.Persistence.Traits;

namespace TenshiShop.Persistence.Models;

public class Brand : IHasTimestamps
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string SlugSource => Name;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
