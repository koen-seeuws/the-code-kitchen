using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IRepository<TEntity> where TEntity : DomainEntity
{
    Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity domainEntity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity domainEntity, CancellationToken cancellationToken = default);
    Task DeleteAsync(object id, CancellationToken cancellationToken = default);
    Task<int> CountAllAsync(CancellationToken cancellationToken = default);
}