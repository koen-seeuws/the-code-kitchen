using Orleans;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IGameManagementGrain : IGrainWithGuidKey
{
    Task<Result<CreateGameResponse>> CreateGame(CreateGameRequest request);
    Task<Result<IEnumerable<GetGameResponse>>> GetGames();
}