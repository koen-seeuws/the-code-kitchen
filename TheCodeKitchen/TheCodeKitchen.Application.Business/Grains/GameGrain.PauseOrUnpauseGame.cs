using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameGrain
{
    private IGrainTimer? _timer;
    
    public async Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame()
    {
        if (state.State.Paused.HasValue)
        {
            state.State.Paused = null;
            _timer = this.RegisterGrainTimer(_ =>
            {
                logger.LogInformation("Logging time from GameGrain {id}: {time}", this.GetPrimaryKey(),
                    DateTimeOffset.UtcNow);
                return Task.CompletedTask;
            }, TimeSpan.Zero, TimeSpan.FromSeconds(1));
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