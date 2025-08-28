using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private Task OnKitchenOrderRatingUpdatedEvent(KitchenOrderRatingUpdatedEvent kitchenOrderRatingUpdatedEvent,
        StreamSequenceToken _)
    {
        double[] ratings = [state.State.Rating, kitchenOrderRatingUpdatedEvent.Rating];
        state.State.Rating = ratings.Average();
        return Task.CompletedTask;
    }
}