using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        logger.LogInformation("Kitchen {kitchenId}: OnNextMomentEvent {moment}", state.State.Id, nextMomentEvent.Moment);
        return Task.CompletedTask;
    }
}