using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Infrastructure.DataAccess;

internal sealed class TheCodeKitchenDbContext(DbContextOptions<TheCodeKitchenDbContext> options) : DbContext(options)
{
    public DbSet<Cook> Cooks { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Kitchen> Kitchens { get; set; }
    public DbSet<KitchenCook> KitchenCooks { get; set; }
    public DbSet<KitchenOrder> KitchenOrders { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Table> Tables { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheCodeKitchenDbContext).Assembly);
    }
}