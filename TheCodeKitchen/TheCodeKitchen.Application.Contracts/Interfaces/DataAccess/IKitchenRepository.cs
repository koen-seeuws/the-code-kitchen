using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IKitchenRepository : IRepository<Kitchen>
{
    Task<IEnumerable<string>> GetAllCodes(CancellationToken cancellationToken);
}