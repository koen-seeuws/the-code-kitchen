namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IGameRepository : IRepository<Game>
{
    TryAsync<Game> GetGameWithKitchensById(Guid gameId, CancellationToken cancellationToken = default);
}