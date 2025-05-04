using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private IGrainTimer? _timer;

    public async Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame()
    {
        if (state.State.Started is null)
            return new EmptyError($"The game with id {this.GetPrimaryKey()} has not yet started");
        
        if (state.State.Paused is not null)
        {
            state.State.Paused = null;
            var speed = TimeSpan.FromSeconds(1) / state.State.SpeedModifier;
            _timer = this.RegisterGrainTimer(_ =>
            {
                logger.LogInformation(
                    "Logging time from GameGrain {id}: {time} (x{modifier})",
                    this.GetPrimaryKey(),
                    DateTimeOffset.UtcNow,
                    state.State.SpeedModifier
                );
                return Task.CompletedTask;
            }, TimeSpan.Zero, speed);
        }
        else
        {
            state.State.Paused = DateTimeOffset.UtcNow;
            _timer?.Dispose();
        }

        await state.WriteStateAsync();

        return mapper.Map<PauseOrUnpauseGameResponse>(state.State);
    }
}