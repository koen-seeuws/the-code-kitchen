using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var offset = TimeSpan.FromMilliseconds(500);
        
        if(nextMomentEvent.NextMomentDelay.HasValue)
            offset = nextMomentEvent.NextMomentDelay.Value / 2;
        
        this.RegisterGrainTimer(async () =>
        {
            var @event = new KitchenRatingUpdatedEvent(state.State.Rating);
            await realTimeKitchenService.SendKitchenRatingUpdatedEvent(state.State.Id, @event);
        }, offset, Timeout.InfiniteTimeSpan);
        
        return Task.CompletedTask;
    }
}