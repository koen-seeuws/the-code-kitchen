using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain
{
    private async Task<Result<TheCodeKitchenUnit>> UpdateKitchenRating()
    {
        ICollection<double> ratings =
        [
            state.State.RequestedFoods
                .Select(r => r.Rating)
                .DefaultIfEmpty(1.0)
                .Average(),
            state.State.DeliveredFoods
                .Select(r => r.Rating)
                .DefaultIfEmpty(0.0)
                .Average(),
            state.State.CompletenessRating
        ];

        var rating = ratings.Average();

        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var stream = streamProvider.GetStream<KitchenOrderRatingUpdatedEvent>(nameof(KitchenOrderRatingUpdatedEvent), state.State.Kitchen);
        var kitchenOrderRatingUpdatedEvent = new KitchenOrderRatingUpdatedEvent(rating);
        await stream.OnNextAsync(kitchenOrderRatingUpdatedEvent);

        return TheCodeKitchenUnit.Value;
    }
}