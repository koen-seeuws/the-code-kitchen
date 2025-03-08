using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class KitchenEntityTypeConfiguration : IEntityTypeConfiguration<KitchenModel>
{
    public void Configure(EntityTypeBuilder<KitchenModel> builder)
    {
        builder.HasKey(kitchen => kitchen.Id);
        
        builder.Property(kitchen => kitchen.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(kitchen => kitchen.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(kitchen => kitchen.Game)
            .WithMany(game => game.Kitchens)
            .HasForeignKey(kitchen => kitchen.GameId)
            .IsRequired();
    }
}