namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IKitchenRepository : IRepository<Kitchen>
{
    Task<ICollection<string>> GetAllCodes(CancellationToken cancellationToken = default);
}