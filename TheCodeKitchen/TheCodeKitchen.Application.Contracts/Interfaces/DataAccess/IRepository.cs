using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IRepository<TEntity> where TEntity : DomainEntity
{
    TryOptionAsync<TEntity> FindByIdAsync(object id, CancellationToken cancellationToken = default);
    TryAsync<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    TryAsync<Seq<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    TryAsync<TEntity> AddAsync(TEntity domainEntity, CancellationToken cancellationToken = default);
    TryAsync<TheCodeKitchenUnit> UpdateAsync(TEntity domainEntity, CancellationToken cancellationToken = default);
    TryAsync<TheCodeKitchenUnit> DeleteAsync(object id, CancellationToken cancellationToken = default);
    TryAsync<int> CountAllAsync(CancellationToken cancellationToken = default);
}