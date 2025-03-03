using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class KitchenOrderEntityTypeConfiguration : IEntityTypeConfiguration<KitchenOrder>
{
    public void Configure(EntityTypeBuilder<KitchenOrder> builder)
    {
        builder.HasKey(ko => new {ko.KitchenId, ko.OrderId});
        
        builder.HasOne(ko => ko.Kitchen)
            .WithMany(k => k.KitchenOrders)
            .HasForeignKey(ko => ko.KitchenId)
            .IsRequired();
        
        builder.HasOne(ko => ko.Order)
            .WithMany(o => o.KitchenOrders)
            .HasForeignKey(ko => ko.OrderId)
            .IsRequired();
    }
}