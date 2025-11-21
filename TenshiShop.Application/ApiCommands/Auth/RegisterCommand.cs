using MediatR;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Domain.Entities;
using TenshiShop.Domain.Enums;

namespace TenshiShop.Application.ApiCommands.Auth;

public record RegisterCommand : IRequest<Result<RegisterResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public UserEntity ToEntity()
    {
        return new UserEntity()
        {
            Name = Name,
            Email = Email,
            Password = Password
        };
    }
}