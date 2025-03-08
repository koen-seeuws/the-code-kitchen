using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

namespace TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

public abstract class Repository<TDomain, TEntity>(DbContext context, IMapper mapper) : IRepository<TDomain>
    where TDomain : class
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public async Task<TDomain?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FindAsync(id, cancellationToken);
        return entity != null ? mapper.Map<TDomain>(entity) : null;
    }

    public async Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await DbSet.ToListAsync(cancellationToken: cancellationToken);
        return mapper.Map<IEnumerable<TDomain>>(entities);
    }

    public async Task<TDomain> AddAsync(TDomain domainEntity, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TEntity>(domainEntity);
        DbSet.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return mapper.Map<TDomain>(entity);
    }

    public async Task UpdateAsync(TDomain domainEntity, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TEntity>(domainEntity);
        
        DbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        
        DbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<long> CountAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet.CountAsync(cancellationToken: cancellationToken);
    }
}