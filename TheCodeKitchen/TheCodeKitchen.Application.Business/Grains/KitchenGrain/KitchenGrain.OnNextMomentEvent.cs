using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var @event = new KitchenRatingUpdatedEvent(state.State.Rating);
        await realTimeKitchenService.SendKitchenRatingUpdatedEvent(state.State.Id, @event);
        await state.WriteStateAsync();
    }
}