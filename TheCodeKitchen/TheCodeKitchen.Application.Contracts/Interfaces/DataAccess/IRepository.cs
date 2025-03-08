using System.Linq.Expressions;

namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IRepository<TDomain> where TDomain : class
{
    Task<TDomain?> GetByIdAsync(object id, CancellationToken cancellationToken);
    Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken);
    Task<TDomain> AddAsync(TDomain domainEntity, CancellationToken cancellationToken);
    Task UpdateAsync(TDomain domainEntity, CancellationToken cancellationToken);
    Task DeleteAsync(object id, CancellationToken cancellationToken);
    Task<long> CountAllAsync(CancellationToken cancellationToken);
}