using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private readonly TimeSpan _deactivationDelayMargin = TimeSpan.FromMinutes(1);
    
    private async Task NextMoment()
    {
        var now = DateTimeOffset.Now;
        
        logger.LogInformation(
            "Logging time from GameGrain {id}: {time} (x{modifier})",
            this.GetPrimaryKey(),
            now,
            state.State.SpeedModifier
        );

        var nextMoment = new NextKitchenMomentRequest(now);
        
        var nextKitchenMomentTasks = state.State.Kitchens.Select(kitchen =>
        {
            var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(kitchen);
            return kitchenGrain.NextMoment(nextMoment);
        });

        await Task.WhenAll(nextKitchenMomentTasks);

        CheckAndDelayDeactivation();
    }
    
    private void CheckAndDelayDeactivation()
    {
        if (!_roundDelay.HasValue) return;
        var delay = _roundDelay.Value.Add(_deactivationDelayMargin);
        DelayDeactivation(delay);
    }
}