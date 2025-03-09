using LanguageExt.ClassInstances.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Infrastructure.DataAccess.Extensions;

namespace TheCodeKitchen.Infrastructure.DataAccess;

public sealed class TheCodeKitchenDbContext(DbContextOptions<TheCodeKitchenDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Kitchen> Kitchens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheCodeKitchenDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        SetBaseEntityProperties();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseEntityProperties();
        return base.SaveChangesAsync(cancellationToken = default);
    }

    private void SetBaseEntityProperties()
    {
        var createdEntities = ChangeTracker.GetEntities(entry => entry.State == EntityState.Added);
        foreach (var entity in createdEntities)
        {
            entity.Modified = DateTimeOffset.UtcNow;
            entity.Created = DateTimeOffset.UtcNow;
        }

        var updatedEntities = ChangeTracker.GetEntities(entry => entry.State == EntityState.Modified);
        foreach (var entity in updatedEntities)
        {
            entity.Modified = DateTimeOffset.UtcNow;
        }
    }
}