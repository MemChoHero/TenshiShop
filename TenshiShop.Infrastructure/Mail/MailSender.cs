using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TenshiShop.Domain.Mail;

namespace TenshiShop.Infrastructure.Mail;

public class MailSender : IMailSender
{
    private readonly MailSettings _settings;

    public MailSender(IOptions<MailSettings> options)
    {
        _settings = options.Value; 
    }

    public async Task SendMail(string receiver, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SmtpUsername, _settings.SmtpUsername));
        email.To.Add(new MailboxAddress("", receiver));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
    }
}

