namespace TenshiShop.Domain.Mail;

public interface IMailSender
{
    Task SendMail(string receiver, string subject, string body);
}

