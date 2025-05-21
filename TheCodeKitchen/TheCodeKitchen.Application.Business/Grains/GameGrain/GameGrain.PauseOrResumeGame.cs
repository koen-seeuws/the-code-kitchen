namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    public async Task<Result<PauseOrResumeGameResponse>> PauseOrUnpauseGame()
    {
        var result = _nextMomentTimer == null ? await ResumeGame() : await PauseGame();

        if (!result.Succeeded)
            return result.Error;

        return new PauseOrResumeGameResponse(_nextMomentTimer == null);
    }
}