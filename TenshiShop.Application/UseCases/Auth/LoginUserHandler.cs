using MediatR;
using Microsoft.AspNetCore.Identity;
using TenshiShop.Application.ApiQueries.Auth;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Abstractions;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Domain.Gateways;

namespace TenshiShop.Application.UseCases.Auth;

public class LoginUserHandler : IRequestHandler<LoginQuery, Result<AuthorizationResponse>>
{
    private readonly IFindUserByEmailGateway _gateway;
    private readonly PasswordHasher<LoginQuery> _passwordHasher;

    public LoginUserHandler(IFindUserByEmailGateway gateway)
    {
        _gateway = gateway;
        _passwordHasher = new PasswordHasher<LoginQuery>();
    }
    
    public async Task<Result<AuthorizationResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var foundUser = await _gateway.FindUserByEmail(request.Email, cancellationToken);
        if (foundUser == null)
        {
            return new Result<AuthorizationResponse>(new Error("User not found"));
        }

        var verificationResult = 
            _passwordHasher.VerifyHashedPassword(request, foundUser.Password, request.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return new Result<AuthorizationResponse>(new Error("Invalid password"));
        }

        return new Result<AuthorizationResponse>(new AuthorizationResponse()
        {
            Email = foundUser.Email,
            Name = foundUser.Name
        });
    }
}