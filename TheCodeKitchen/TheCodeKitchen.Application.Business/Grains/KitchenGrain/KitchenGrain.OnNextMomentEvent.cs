using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var offset = nextMomentEvent.TimePerMoment / 2;
        
        this.RegisterGrainTimer(async () =>
        {
            var rating = state.State.OrderRatings.Values.DefaultIfEmpty(1.0).Average();
            var @event = new KitchenRatingUpdatedEvent(rating);
            await realTimeKitchenService.SendKitchenRatingUpdatedEvent(state.State.Id, @event);
        }, offset, Timeout.InfiniteTimeSpan);
        
        return Task.CompletedTask;
    }
}