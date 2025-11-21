using Microsoft.EntityFrameworkCore;
using TenshiShop.Domain.Entities;
using TenshiShop.Domain.Enums;
using TenshiShop.Domain.Gateways;
using TenshiShop.Persistence.Models;

namespace TenshiShop.Persistence.DataAccess;

public class UserDataAccess : ICreateUserGateway, IFindUserByEmailGateway
{
    private readonly AppDbContext _dbContext;

    public UserDataAccess(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserEntity> CreateUser(UserEntity userEntity, RoleEnum roleEnum, CancellationToken ct)
    {
        var user = User.FromEntity(userEntity);
        var role = Role.FromEnum(roleEnum);
        
        var roleFromDb = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == role.Name, ct);
        if (roleFromDb == null)
        {
            roleFromDb = role;
            await _dbContext.Roles.AddAsync(roleFromDb, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        user.Roles.Add(roleFromDb);
        
        var userResult = (await _dbContext.Users.AddAsync(user, ct)).Entity;
        await _dbContext.SaveChangesAsync(ct);

        return userResult.ToEntity();
    }

    public async Task<UserEntity?> FindUserByEmail(string email, CancellationToken ct)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
        
        return result?.ToEntity();
    }
}