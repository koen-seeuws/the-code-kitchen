using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Server.Core.Domain;
using TheCodeKitchen.Server.Core.Domain.Entities;

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
    }
}