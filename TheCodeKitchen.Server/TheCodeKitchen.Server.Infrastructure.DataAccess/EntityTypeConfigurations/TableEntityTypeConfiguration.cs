using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Server.Core.Domain.Entities;

namespace TheCodeKitchen.Server.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class TableEntityTypeConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Spots).IsRequired();
        builder.Property(t => t.Number).IsRequired();
        
        builder.HasIndex(t => new {t.GameId, t.Number}).IsUnique();

        builder.HasOne(t => t.Game)
            .WithMany(g => g.Tables)
            .HasForeignKey(t => t.GameId)
            .IsRequired();
    }
}