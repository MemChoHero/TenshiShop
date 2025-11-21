using TenshiShop.Domain.Entities;
using TenshiShop.Domain.Enums;

namespace TenshiShop.Application.ApiResponses.Auth;

public class RegisterResponse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<RoleEnum> Roles { get; set; } = [];

    public static RegisterResponse FromEntity(UserEntity request)
    {
        return new RegisterResponse()
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.Password,
            Roles = request.Roles
        };
    }
}