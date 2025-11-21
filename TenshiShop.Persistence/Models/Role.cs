using TenshiShop.Domain.Enums;

namespace TenshiShop.Persistence.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<User> Users { get; set; } = [];

    public static Role FromEnum(RoleEnum roleEnum)
    {
        return new Role()
        {
            Name = roleEnum.ToString()
        };
    }

    public RoleEnum ToEnum()
    {
        return Enum.Parse<RoleEnum>(Name);
    }
}