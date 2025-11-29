using MediatR;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Auth;

namespace TenshiShop.Application.ApiCommands.Auth;

public class ActivateUserCommand : IRequest<Result<ActivateUserResponse>>
{
    public string Code { get; set; } = string.Empty;
}