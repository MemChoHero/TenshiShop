using TenshiShop.Domain.Entities;
using TenshiShop.Domain.Enums;

namespace TenshiShop.Domain.Gateways;

public interface ICreateUserGateway
{
    Task<UserEntity> CreateUser(UserEntity user, RoleEnum role, CancellationToken ct);
}