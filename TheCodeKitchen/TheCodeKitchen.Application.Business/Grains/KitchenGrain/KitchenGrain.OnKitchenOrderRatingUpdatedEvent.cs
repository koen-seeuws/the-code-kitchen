using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task OnKitchenOrderRatingUpdatedEvent(KitchenOrderRatingUpdatedEvent kitchenOrderRatingUpdatedEvent,
        StreamSequenceToken _)
    {
        ICollection<double> ratings = [state.State.Rating, kitchenOrderRatingUpdatedEvent.Rating];
        state.State.Rating = ratings.Average();
        await state.WriteStateAsync();

        var @event = new KitchenRatingUpdatedEvent(state.State.Rating);
        await realTimeKitchenService.SendKitchenRatingUpdatedEvent(state.State.Id, @event);
    }
}