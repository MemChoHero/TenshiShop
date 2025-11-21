namespace TenshiShop.Persistence.Traits;

public interface IHasTimestamps
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}