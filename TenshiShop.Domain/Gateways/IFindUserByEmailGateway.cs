using TenshiShop.Domain.Entities;

namespace TenshiShop.Domain.Gateways;

public interface IFindUserByEmailGateway
{
    Task<UserEntity?> FindUserByEmail(string email, CancellationToken ct);
}