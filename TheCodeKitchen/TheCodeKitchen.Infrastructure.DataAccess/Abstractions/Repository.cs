using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

public abstract class Repository<TEntity>(TheCodeKitchenDbContext context) : IRepository<TEntity>
    where TEntity : DomainEntity
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public async Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync([id], cancellationToken);

    public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FindAsync([id], cancellationToken);
        if (entity == null)
            throw new NotFoundException($"{typeof(TEntity).Name} was not found using id {id}");
        return entity;
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.ToListAsync(cancellationToken);

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        DbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.CountAsync(cancellationToken);
}