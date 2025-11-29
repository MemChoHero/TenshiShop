using MediatR;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Abstractions;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Domain.Gateways;
using TenshiShop.Domain.Redis;

namespace TenshiShop.Application.UseCases.Auth;

public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, Result<ActivateUserResponse>>
{
    private readonly IActivateUserGateway _gateway;
    private readonly IRedisGetter _redisGetter;

    public ActivateUserHandler(
        IActivateUserGateway gateway,
        IRedisGetter redisGetter)
    {
        _gateway = gateway;
        _redisGetter = redisGetter;
    }

    public async Task<Result<ActivateUserResponse>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var email = await _redisGetter.Get(request.Code, cancellationToken);
        if (email == null)
        {
            return new Result<ActivateUserResponse>(new Error("incorrect activate code: value email not found"));
        }

        await _gateway.ActivateUser((string) email, cancellationToken);

        return new Result<ActivateUserResponse>(new ActivateUserResponse { Email = (string) email });
    }
}