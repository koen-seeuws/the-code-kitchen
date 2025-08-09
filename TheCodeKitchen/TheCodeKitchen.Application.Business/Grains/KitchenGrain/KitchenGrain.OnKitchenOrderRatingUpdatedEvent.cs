using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task OnKitchenOrderRatingUpdatedEvent(KitchenOrderRatingUpdatedEvent nextMomentEvent, StreamSequenceToken _)
    {
        ICollection<double> ratings = [state.State.Rating, nextMomentEvent.Rating];
        state.State.Rating = ratings.Average();
        await state.WriteStateAsync();
    }
}