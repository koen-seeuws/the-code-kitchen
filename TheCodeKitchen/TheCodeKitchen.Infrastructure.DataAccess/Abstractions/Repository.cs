using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Domain.Exceptions;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

public abstract class Repository<TEntity>(TheCodeKitchenDbContext context) : IRepository<TEntity>
    where TEntity : DomainEntity
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public TryOptionAsync<TEntity> FindByIdAsync(object id, CancellationToken cancellationToken = default)
        => TryOptionAsync(async () =>
        {
            var entity = await DbSet.FindAsync([id], cancellationToken);
            return entity != null ? Some(entity) : None;
        });


    public TryAsync<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        => TryAsync(async () =>
        {
            var entity = await DbSet.FindAsync([id], cancellationToken);
            if (entity == null)
                throw new NotFoundException($"{typeof(TEntity).Name} was not found using id {id}");
            return entity;
        });


    public TryAsync<Seq<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => TryAsync(async () =>
        {
            var entities = await DbSet.ToListAsync(cancellationToken);
            return entities.ToSeq();
        });

    public TryAsync<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => TryAsync(async () =>
        {
            DbSet.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        });


    public TryAsync<TheCodeKitchenUnit> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        => TryAsync(async () =>
        {
            DbSet.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return TheCodeKitchenUnit.Value;
        });

    public TryAsync<TheCodeKitchenUnit> DeleteAsync(object id, CancellationToken cancellationToken = default)
        => GetByIdAsync(id, cancellationToken)
            .Bind(entity => TryAsync(async () =>
            {
                DbSet.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
                return TheCodeKitchenUnit.Value;
            }));

    public TryAsync<int> CountAllAsync(CancellationToken cancellationToken = default)
        => TryAsync(async () => await DbSet.CountAsync(cancellationToken));
}