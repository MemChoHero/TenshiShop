namespace TenshiShop.Domain.Gateways;

public interface IActivateUserGateway
{
    Task ActivateUser(string email, CancellationToken ct);
}