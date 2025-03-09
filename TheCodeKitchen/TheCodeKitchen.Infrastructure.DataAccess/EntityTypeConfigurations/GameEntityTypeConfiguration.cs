using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Infrastructure.DataAccess.EntityTypeConfigurations;

internal sealed class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        
        builder.HasKey(game => game.Id);

        builder.Property(game => game.Name).IsRequired();
    }
}