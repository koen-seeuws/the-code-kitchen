using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Server.Core.Domain;
using TheCodeKitchen.Server.Core.Domain.Entities;

namespace TheCodeKitchen.Server.Infrastructure.DataAccess;

internal sealed class TheCodeKitchenDbContext : DbContext
{
    public DbSet<Cook> Cooks { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Kitchen> Kitchens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheCodeKitchenDbContext).Assembly);
    }
}