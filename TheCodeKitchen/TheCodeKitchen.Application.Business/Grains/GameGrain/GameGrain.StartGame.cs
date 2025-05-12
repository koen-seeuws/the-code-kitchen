namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    public async Task<Result<TheCodeKitchenUnit>> StartGame()
    {
        if (state.State.Started is not null)
            return new GameAlreadyStartedError($"The game with id {this.GetPrimaryKey()} has already started");
        
        if (state.State.Kitchens.Count is 0)
            return new EmptyError($"The game with id {this.GetPrimaryKey()} has no kitchens");

        state.State.Paused = state.State.Started = DateTimeOffset.UtcNow;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}