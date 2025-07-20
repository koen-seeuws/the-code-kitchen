namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        if (state.State.Completed) 
            return;
        
        // Time order has been active
        var time = state.State.Time += TheCodeKitchenMomentDuration.Value;
        
        // Rating down order when it takes too long
        var nonDeliveredRequestedFoodRatings = state.State.FoodRequestRatings
            .Where(fr => !fr.Delivered)
            .ToList();

        foreach (var foodRating in nonDeliveredRequestedFoodRatings)
        {
            var margin = 0.1 * foodRating.MinimumTimeToPrepareFood;
            var graceTime = foodRating.MinimumTimeToPrepareFood + margin;

            if (time <= graceTime)
                continue; // Still within required time + margin

            var overTime = time - graceTime;
            var penaltyPercent = overTime / foodRating.MinimumTimeToPrepareFood;

            foodRating.Rating = Math.Max(0, 100 * (1 - penaltyPercent)); // Rating cannot go below 0
        }
    }
}