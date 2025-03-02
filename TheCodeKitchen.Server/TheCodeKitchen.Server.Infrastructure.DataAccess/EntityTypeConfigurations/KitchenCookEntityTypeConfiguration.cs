using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Server.Core.Domain.Entities;

namespace TheCodeKitchen.Server.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class KitchenCookEntityTypeConfiguration : IEntityTypeConfiguration<KitchenCook>
{
    public void Configure(EntityTypeBuilder<KitchenCook> builder)
    {
        builder.HasOne(kc => kc.Kitchen)
            .WithMany(k => k.KitchenCooks)
            .HasForeignKey(kc => kc.KitchenId)
            .IsRequired();
        
        builder.HasOne(kc => kc.Cook)
            .WithMany(c => c.KitchenCooks)
            .HasForeignKey(kc => kc.CookId)
            .IsRequired();
    }
}