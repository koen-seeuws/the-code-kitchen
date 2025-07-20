using TheCodeKitchen.Application.Contracts.Response.Game;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    public async Task<Result<PauseOrResumeGameResponse>> PauseOrUnpauseGame()
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");
        
        var result = _nextMomentTimer is null ? await ResumeGame() : await PauseGame();

        if (!result.Succeeded)
            return result.Error;

        return new PauseOrResumeGameResponse(_nextMomentTimer is null);
    }
}