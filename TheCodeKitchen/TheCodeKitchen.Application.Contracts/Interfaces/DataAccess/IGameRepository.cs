namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface IGameRepository : IRepository<Game>
{
    Task<Game> GetGameWithKitchensById(Guid gameId, CancellationToken cancellationToken = default);
    Task<Game> GetGameWithKitchensAndCooksByCode(string joinCode, CancellationToken cancellationToken = default);
}