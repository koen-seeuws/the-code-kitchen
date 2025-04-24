using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Infrastructure.DataAccess.Extensions;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

public class CookEntityTypeConfiguration : IEntityTypeConfiguration<Cook>
{
    public void Configure(EntityTypeBuilder<Cook> builder)
    {
        builder.ToTable("Cooks");
        builder.HasGuidId();
        
        builder.Property(cook => cook.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(cook => cook.Kitchen)
            .WithMany(kitchen => kitchen.Cooks)
            .IsRequired();
    }
}