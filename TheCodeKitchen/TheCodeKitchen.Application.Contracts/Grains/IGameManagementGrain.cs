namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IGameManagementGrain : IGrainWithGuidKey
{
    Task<Result<CreateGameResponse>> CreateGame(CreateGameRequest request);
    Task<Result<IEnumerable<GetGameResponse>>> GetGames();
}