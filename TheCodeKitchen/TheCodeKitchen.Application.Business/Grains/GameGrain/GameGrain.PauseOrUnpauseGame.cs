using Orleans.Providers.Streams.Common;
using Orleans.Streams;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private IGrainTimer? _timer;
    private TimeSpan? _roundDelay;
    private IAsyncStream _gameStream;

    public async Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame()
    {
        if (state.State.Started is null)
            return new EmptyError($"The game with id {this.GetPrimaryKey()} has not yet started");
        
        if (state.State.Paused is not null)
        {
            state.State.Paused = null;
            _roundDelay = TimeSpan.FromSeconds(1) / state.State.SpeedModifier;
            _timer = this.RegisterGrainTimer(NextMoment, TimeSpan.Zero, _roundDelay.Value);
        }
        else
        {
            state.State.Paused = DateTimeOffset.UtcNow;
            _roundDelay = TimeSpan.Zero;
            _timer?.Dispose();
        }

        await state.WriteStateAsync();

        return mapper.Map<PauseOrUnpauseGameResponse>(state.State);
    }
}