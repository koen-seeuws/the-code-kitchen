using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Requests.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeliverFood(DeliverFoodRequest request)
    {
        if (!state.RecordExists)
        {
            var orderNumber = this.GetPrimaryKeyLong();
            var kitchenId = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
            return new NotFoundError($"The order with number {orderNumber} does not exist in kitchen {kitchenId}");
        }
        
        if (state.State.Completed)
            return new OrderAlreadyCompletedError($"The order with number {state.State.Number} has already been completed, you cannot deliver any more food to it");
        
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var releaseFoodResult = await cookGrain.ReleaseFood();
        if (!releaseFoodResult.Succeeded)
            return releaseFoodResult.Error;
        
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(releaseFoodResult.Value.Food);
        var getFoodResult = await foodGrain.GetFood();
        if (!getFoodResult.Succeeded)
            return getFoodResult.Error;
        var food = getFoodResult.Value;
        
        var setOrderRequest = new SetOrderRequest(request.Cook, state.State.Number);
        var setOrderResult = await foodGrain.SetOrder(setOrderRequest);
        if (!setOrderResult.Succeeded)
            return setOrderResult.Error;
        
        // Rating the delivered food quality
        var qualityRating = 100.0; //TODO: calculate quality rating based on food properties
        
        
        var foodDelivery = new FoodDelivery(food.Id, food.Name, qualityRating);
        state.State.DeliveredFoods.Add(foodDelivery);
        
        // Making sure the customer is no longer waiting for its food (wont be picked up anymore OnNextMoment)
        var foodRequestRating = state.State.FoodRequestRatings
            .Where(d => !d.Delivered)
            .FirstOrDefault(d => d.RequestedFood.Equals(food.Name, StringComparison.OrdinalIgnoreCase));
        
        if (foodRequestRating != null)
            foodRequestRating.Delivered = true;
        
        // Rating order completeness
        var requestedFoods = state.State.FoodRequestRatings
            .Select(d => d.RequestedFood)
            .ToList();
        
        var deliveredFoods = state.State.DeliveredFoods
            .Select(d => d.Food)
            .ToList();
        
        var missingFoods = requestedFoods.MultiExcept(deliveredFoods).ToList();
        var wrongFoods = deliveredFoods.MultiExcept(requestedFoods).ToList();
        var correctFoods = requestedFoods.MultiIntersect(deliveredFoods).ToList();
        
        // In case of missing foods, we apply a penalty for wrong foods
        // In case of no missing food (and possibly extra food), we apply no penalty
        var penaltyWeight = (double) missingFoods.Count / requestedFoods.Count; 
        
        var adjustedCorrectCount = correctFoods.Count - (wrongFoods.Count * penaltyWeight);
        adjustedCorrectCount = Math.Max(0, adjustedCorrectCount); // Avoid negative score

        state.State.CompletenessRating = adjustedCorrectCount / requestedFoods.Count * 100;
        
        // Update state
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}