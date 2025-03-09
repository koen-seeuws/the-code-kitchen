namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IKitchenRepository : IRepository<Kitchen>
{
    TryAsync<Seq<string>> GetAllCodes(CancellationToken cancellationToken = default);
}