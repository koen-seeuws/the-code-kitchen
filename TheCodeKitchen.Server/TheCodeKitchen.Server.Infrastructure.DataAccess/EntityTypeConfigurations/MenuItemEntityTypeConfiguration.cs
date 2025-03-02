using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Server.Core.Domain.Entities;

namespace TheCodeKitchen.Server.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class MenuItemEntityTypeConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(mi => new {mi.MenuId, mi.ItemId});

        builder.Property(mi => mi.Price).IsRequired();
        
        builder.HasOne(mi => mi.Menu)
            .WithMany(m => m.MenuItems)
            .HasForeignKey(mi => mi.MenuId)
            .IsRequired();
        
        builder.HasOne(mi => mi.Item)
            .WithMany(i => i.MenuItems)
            .HasForeignKey(mi => mi.ItemId)
            .IsRequired();
    }
}