using MediatR;
using Microsoft.AspNetCore.Identity;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Auth;
using TenshiShop.Application.Validation.Auth;
using TenshiShop.Domain.Enums;
using TenshiShop.Domain.Gateways;
using TenshiShop.Domain.Mail;
using TenshiShop.Domain.Redis;

namespace TenshiShop.Application.UseCases.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly ICreateUserGateway _gateway;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IMailSender _mailSender;
    private readonly IRedisSaver _redisSaver;

    private readonly PasswordHasher<RegisterCommand> _passwordHasher;

    public RegisterUserHandler(
        ICreateUserGateway gateway, 
        IValidator<RegisterCommand> validator,
        IMailSender mailSender,
        IRedisSaver redisSaver)
    {
        _gateway = gateway;
        _validator = validator;
        _mailSender = mailSender;
        _redisSaver = redisSaver;

        _passwordHasher = new PasswordHasher<RegisterCommand>();
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateOrThrow(request);
        
        request.Password = _passwordHasher.HashPassword(request, request.Password);
        
        var createdUser = await _gateway.CreateUser(request.ToEntity(), RoleEnum.User, cancellationToken);

        var linkGuid = Guid.NewGuid().ToString();

        await _redisSaver.SaveString(linkGuid, createdUser.Email, TimeSpan.FromHours(12), cancellationToken);

        await _mailSender.SendMail(
            createdUser.Email, 
            "Регистрация в TenshiShop", $"<h1>Спасибо за регистрацию, {createdUser.Name}!</h1>. Для активации аккаунта перейдите по ссылке: https://localhost:7137/api/auth/confirm?code={linkGuid}");

        return new Result<RegisterResponse>(RegisterResponse.FromEntity(createdUser));
    }
}