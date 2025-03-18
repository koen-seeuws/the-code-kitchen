namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IKitchenRepository : IRepository<Kitchen>
{
    Task<bool> CodeExists(string code, CancellationToken cancellationToken = default);
}