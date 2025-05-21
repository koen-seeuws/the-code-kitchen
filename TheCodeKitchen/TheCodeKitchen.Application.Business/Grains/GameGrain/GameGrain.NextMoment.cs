using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private IGrainTimer? _nextMomentTimer;
    private TimeSpan? _nextMomentDelay;
    
    private async Task NextMoment()
    {
        var now = DateTimeOffset.Now;
        
        logger.LogInformation(
            "Logging time from GameGrain {id}: {time} (x{modifier})",
            this.GetPrimaryKey(),
            now,
            state.State.SpeedModifier
        );

        // Notifying the kitchens
        var nextMoment = new NextKitchenMomentRequest(now);
        
        var nextKitchenMomentTasks = state.State.Kitchens.Select(kitchen =>
        {
            var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(kitchen);
            return kitchenGrain.NextMoment(nextMoment);
        });

        await Task.WhenAll(nextKitchenMomentTasks);
        
        // Order generation
        if (--_secondsUntilNewOrder <= 0)
        {
            await GenerateOrder();
            await PickSecondsUntilNextOrder();
        }

        // Keep grain active while game is playing
        await CheckAndDelayDeactivation();
    }
    
    private Task CheckAndDelayDeactivation()
    {
        if (!_nextMomentDelay.HasValue) return Task.CompletedTask;
        var delay = _nextMomentDelay.Value.Add(TimeSpan.FromSeconds(30));
        DelayDeactivation(delay);
        return Task.CompletedTask;
    }
}