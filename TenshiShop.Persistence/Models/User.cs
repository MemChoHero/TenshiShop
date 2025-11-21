using TenshiShop.Domain.Entities;
using TenshiShop.Persistence.Traits;

namespace TenshiShop.Persistence.Models;

public class User : IHasTimestamps
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } =  string.Empty;
    
    public List<Role> Roles { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static User FromEntity(UserEntity entity)
    {
        return new User()
        {
            Name = entity.Name,
            Email = entity.Email,
            Password = entity.Password
        };
    }

    public UserEntity ToEntity()
    {
        return new UserEntity()
        {
            Name = Name,
            Email = Email,
            Password = Password,
            Roles = Roles.Select(r => r.ToEnum()).ToList()
        };
    }
}