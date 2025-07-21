namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    private async Task<Result<TheCodeKitchenUnit>> UpdateKitchenRating()
    {
        ICollection<double> ratings =
        [
            state.State.FoodRequestRatings.Select(r => r.Rating).Average(),
            state.State.DeliveredFoods.Select(r => r.Rating).Average(),
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