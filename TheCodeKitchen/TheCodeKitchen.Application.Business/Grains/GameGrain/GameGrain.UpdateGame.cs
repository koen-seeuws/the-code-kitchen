using TheCodeKitchen.Application.Contracts.Requests.Game;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    public async Task<Result<TheCodeKitchenUnit>> UpdateGame(UpdateGameRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");

        var running = _nextMomentTimer != null;

        if (running)
            await PauseOrUnpauseGame();
        
        state.State.SpeedModifier = request.SpeedModifier;

        await state.WriteStateAsync();
        
        if (running)
            await PauseOrUnpauseGame();

        return TheCodeKitchenUnit.Value;
    }
}