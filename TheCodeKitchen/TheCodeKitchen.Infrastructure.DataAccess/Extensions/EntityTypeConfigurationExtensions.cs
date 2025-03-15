using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Extensions;

public static class EntityTypeConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> HasGuidId<TEntity>(this EntityTypeBuilder<TEntity> builder)
    where TEntity : class, IHasGuidId
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedNever();
        return builder;
    }
}