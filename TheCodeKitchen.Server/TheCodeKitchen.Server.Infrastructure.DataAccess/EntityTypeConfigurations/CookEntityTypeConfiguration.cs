using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Server.Core.Domain;

namespace TheCodeKitchen.Server.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class CookEntityTypeConfiguration : IEntityTypeConfiguration<Cook>
{
    public void Configure(EntityTypeBuilder<Cook> builder)
    {
        builder.HasKey();
        
        builder.Property(c => c.Username)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.PasswordHash).IsRequired();
        
        builder.HasOne(c => c.Kitchen)
            .WithMany(k => k.Cooks)
            .HasForeignKey(c => c.KitchenId);
    }
}