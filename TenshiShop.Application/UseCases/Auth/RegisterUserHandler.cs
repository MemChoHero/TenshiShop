using MediatR;
using Microsoft.AspNetCore.Identity;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Application.Validation.Auth;
using TenshiShop.Domain.Enums;
using TenshiShop.Domain.Gateways;

namespace TenshiShop.Application.UseCases.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly ICreateUserGateway _gateway;
    private readonly PasswordHasher<RegisterCommand> _passwordHasher;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterUserHandler(ICreateUserGateway gateway, IValidator<RegisterCommand> validator)
    {
        _gateway = gateway;
        _validator = validator;
        _passwordHasher = new PasswordHasher<RegisterCommand>();
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateOrThrow(request);
        
        request.Password = _passwordHasher.HashPassword(request, request.Password);
        
        var createdUser = await _gateway.CreateUser(request.ToEntity(), RoleEnum.User, cancellationToken);

        return new Result<RegisterResponse>(RegisterResponse.FromEntity(createdUser));
    }
}