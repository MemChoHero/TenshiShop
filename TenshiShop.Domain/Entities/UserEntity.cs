using TenshiShop.Domain.Enums;

namespace TenshiShop.Domain.Entities;

public class UserEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public List<RoleEnum> Roles { get; set; } = [];
}