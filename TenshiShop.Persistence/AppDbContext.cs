using Microsoft.EntityFrameworkCore;
using TenshiShop.Persistence.Interceptors;
using TenshiShop.Persistence.Models;

namespace TenshiShop.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(
            new TimestampsInterceptor()
        );
        base.OnConfiguring(optionsBuilder);
    }
}