namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    public Task<Result<GetGameResponse>> GetGame()
    {
        Result<GetGameResponse> result = state.RecordExists
            ? mapper.Map<GetGameResponse>(state.State)
            : new NotFoundError($"The game with id {this.GetPrimaryKey()} does not exist");
        return Task.FromResult(result);
    }
}