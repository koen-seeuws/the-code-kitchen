namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private IGrainTimer? _nextMomentTimer;
    private TimeSpan? _roundDelay;

    public async Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame()
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");
        
        if (state.State.Started is null)
            return new EmptyError($"The game with id {this.GetPrimaryKey()} has not yet started");
        
        if (_nextMomentTimer is null)
        {
            _roundDelay = TimeSpan.FromSeconds(1) / state.State.SpeedModifier;
            if (_secondsUntilNewOrder is null)
                await PickSecondsUntilNextOrder();
            _nextMomentTimer = this.RegisterGrainTimer(NextMoment, TimeSpan.Zero, _roundDelay.Value);
        }
        else
        {
            _nextMomentTimer?.Dispose();
            _nextMomentTimer = null;
        }

        return new PauseOrUnpauseGameResponse(_nextMomentTimer == null);
    }
}