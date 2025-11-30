namespace TenshiShop.Application.Permissions;

public interface IActiveChecker
{
    Task<bool> CheckOrSendMail(string email, CancellationToken ct);
}
