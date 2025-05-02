using Orleans;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IGameGrain : IGrainWithGuidKey
{
    Task<Result<CreateGameResponse>> Initialize(CreateGameRequest request, int count);
    Task<Result<CreateKitchenResponse>> AddKitchen(CreateKitchenRequest request);
    Task<Result<JoinGameResponse>> JoinGame(JoinGameRequest request);
    Task<Result<TheCodeKitchenUnit>> StartGame();
    Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame();
    Task<Result<GetGameResponse>> GetGame();
    Task<Result<IEnumerable<GetKitchenResponse>>> GetKitchens();
}