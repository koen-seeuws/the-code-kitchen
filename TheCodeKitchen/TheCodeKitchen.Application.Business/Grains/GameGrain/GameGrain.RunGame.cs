using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private readonly TimeSpan _deactivationDelayMargin = TimeSpan.FromMinutes(1);
    
    private Task RunGame()
    {
        logger.LogInformation(
            "Logging time from GameGrain {id}: {time} (x{modifier})",
            this.GetPrimaryKey(),
            DateTimeOffset.UtcNow,
            state.State.SpeedModifier
        );
        

        CheckAndDelayDeactivation();
        return Task.CompletedTask;
    }
    
    private void CheckAndDelayDeactivation()
    {
        if (!_roundDelay.HasValue) return;
        var delay = _roundDelay.Value.Add(_deactivationDelayMargin);
        DelayDeactivation(delay);
    }
}