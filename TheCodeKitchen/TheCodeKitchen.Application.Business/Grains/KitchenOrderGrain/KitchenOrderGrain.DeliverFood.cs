using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Requests.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeliverFood(DeliverFoodRequest request)
    {
        if (state.State.Completed)
            return new OrderAlreadyCompletedError($"The order with number {state.State.Number} has already been completed, you cannot deliver any more food to it");
        
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var releaseFoodResult = await cookGrain.ReleaseFood();

        if (!releaseFoodResult.Succeeded)
            return releaseFoodResult.Error;
        
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(releaseFoodResult.Value.Food);
        var setOrderRequest =
            new SetOrderRequest(request.Cook, state.State.Number);
        
        var setOrderResult = await foodGrain.SetOrder(setOrderRequest);
        
        if (!setOrderResult.Succeeded)
            return setOrderResult.Error;
        
        state.State.DeliveredFoods.Add(releaseFoodResult.Value.Food);
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}