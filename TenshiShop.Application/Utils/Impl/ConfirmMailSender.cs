using TenshiShop.Domain.Mail;
using TenshiShop.Domain.Redis;

namespace TenshiShop.Application.Utils.Impl;

public class ConfirmMailSender : IConfirmMailSender
{
    private readonly IMailSender _mailSender;
    private readonly IRedisSaver _redisSaver;

    public ConfirmMailSender(IMailSender mailSender, IRedisSaver redisSaver)
    {
   
        _mailSender = mailSender;
        _redisSaver = redisSaver;
    }

    public async Task Send(string receiver, string name, CancellationToken ct)
    {
        var linkGuid = Guid.NewGuid().ToString();

        await _redisSaver.SaveString(linkGuid, receiver, TimeSpan.FromHours(12), ct);

        await _mailSender.SendMail(
            receiver,
            "Регистрация в TenshiShop", $"<h1>Спасибо за регистрацию, {name}!</h1>. Для активации аккаунта перейдите по ссылке: https://localhost:7137/api/auth/confirm?code={linkGuid}");
    }
}