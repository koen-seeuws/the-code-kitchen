using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Infrastructure.DataAccess.Extensions;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class KitchenEntityTypeConfiguration : IEntityTypeConfiguration<Kitchen>
{
    public void Configure(EntityTypeBuilder<Kitchen> builder)
    {
        builder.ToTable("Kitchens");
        builder.HasGuidId();
        
        builder.Property(kitchen => kitchen.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(kitchen => kitchen.Code)
            .HasMaxLength(100);

        builder.HasIndex(kitchen => kitchen.Code).IsUnique();

        builder.HasOne(kitchen => kitchen.Game)
            .WithMany(game => game.Kitchens)
            .HasForeignKey(kitchen => kitchen.GameId)
            .IsRequired();
    }
}