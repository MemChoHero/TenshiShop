using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TenshiShop.Persistence.Traits;

namespace TenshiShop.Persistence.Interceptors;

public class TimestampsInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is {} dbContext)
        {
            var createdEntries = dbContext.ChangeTracker
                .Entries<IHasTimestamps>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in createdEntries)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            
            var updatedEntries = dbContext.ChangeTracker
                .Entries<IHasTimestamps>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in updatedEntries)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}