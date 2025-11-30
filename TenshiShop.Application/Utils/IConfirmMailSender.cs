namespace TenshiShop.Application.Utils;

public interface IConfirmMailSender
{
    Task Send(string receiver, string name, CancellationToken ct);
}