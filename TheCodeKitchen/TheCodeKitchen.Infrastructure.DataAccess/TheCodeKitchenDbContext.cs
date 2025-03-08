using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess;

internal sealed class TheCodeKitchenDbContext(DbContextOptions<TheCodeKitchenDbContext> options) : DbContext(options)
{
    public DbSet<GameModel> Games { get; set; }
    public DbSet<KitchenModel> Kitchens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheCodeKitchenDbContext).Assembly);
    }
}