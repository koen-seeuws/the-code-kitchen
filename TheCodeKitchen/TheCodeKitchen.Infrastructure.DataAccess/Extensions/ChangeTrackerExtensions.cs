using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Extensions;

public static class ChangeTrackerExtensions
{
    public static IEnumerable<DomainObject> GetEntities(this ChangeTracker changeTracker, Func<EntityEntry, bool> predicate)
    {
        return changeTracker
            .Entries()
            .Where(predicate)
            .Select(entry => entry.Entity)
            .OfType<DomainObject>();
    }
}