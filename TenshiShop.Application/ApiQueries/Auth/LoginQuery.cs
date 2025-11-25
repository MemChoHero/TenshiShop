using MediatR;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Domain.Entities;

namespace TenshiShop.Application.ApiQueries.Auth;

public class LoginQuery : IRequest<Result<AuthorizationResponse>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public UserEntity ToEntity()
    {
        return new UserEntity()
        {
            Email = Email,
            Password = Password
        };
    }
}