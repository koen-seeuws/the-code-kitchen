namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IGameGrain : IGrainWithGuidKey
{
    Task<Result<CreateKitchenResponse>> CreateKitchen(CreateKitchenRequest request);
    Task<Result<GetGameResponse>> GetGame();
    Task<Result<IEnumerable<GetKitchenResponse>>> GetKitchens();
    Task<Result<CreateGameResponse>> Initialize(CreateGameRequest request, int count);
    Task<Result<TheCodeKitchenUnit>> PauseGame();
    Task<Result<PauseOrResumeGameResponse>> PauseOrUnpauseGame();
    Task<Result<TheCodeKitchenUnit>> ResumeGame();
    Task<Result<TheCodeKitchenUnit>> StartGame();
    Task<Result<TheCodeKitchenUnit>> UpdateGame(UpdateGameRequest request);
}