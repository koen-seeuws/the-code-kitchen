using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class KitchenEntityTypeConfiguration : IEntityTypeConfiguration<Kitchen>
{
    public void Configure(EntityTypeBuilder<Kitchen> builder)
    {
        builder.HasKey(k => k.Id);
        
        builder.Property(k => k.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(k => k.Game)
            .WithMany(g => g.Kitchens)
            .HasForeignKey(k => k.GameId)
            .IsRequired();
    }
}