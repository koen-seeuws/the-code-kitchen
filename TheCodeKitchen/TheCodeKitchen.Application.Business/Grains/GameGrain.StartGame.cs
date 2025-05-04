using Orleans;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameGrain
{
    public async Task<Result<TheCodeKitchenUnit>> StartGame()
    {
        if (state.State.Started is not null)
            return new GameAlreadyStartedError($"The game with id {this.GetPrimaryKey()} has already started");

        state.State.Paused = state.State.Started = DateTimeOffset.UtcNow;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}