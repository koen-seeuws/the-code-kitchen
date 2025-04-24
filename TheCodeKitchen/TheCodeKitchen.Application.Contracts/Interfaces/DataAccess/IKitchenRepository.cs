namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IKitchenRepository : IRepository<Kitchen>
{
    Task<bool> CodeExists(string code, CancellationToken cancellationToken = default);
    Task<ICollection<Kitchen>> GetByGameId(Guid gameId, CancellationToken cancellationToken = default);
}