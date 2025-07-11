using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request)
    {
        if (state.State.Foods.Count <= 0)
            return new EquipmentEmptyError($"The equipment {state.State.EquipmentType} {state.State.Number} does not contain any food");

        if (state.State.Foods.Count > 1)
        {
            var mixResult = await MixFood();
            if(!mixResult.Succeeded)
                return mixResult.Error;
        }

        // TODO: Handling single/merged food item and/or steps from processing in this equipment
        
        var food = state.State.Foods.First();
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var holdFoodRequest = new HoldFoodRequest(food);

        var holdFoodResult = await cookGrain.HoldFood(holdFoodRequest);
        
        if(!holdFoodResult.Succeeded)
            return holdFoodResult.Error;
        
        //TODO: Check if this can be improved (that this call becomes unnecessary)
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(state.State.Foods.First());
        var getFoodResult = await foodGrain.GetFood();
        if (!getFoodResult.Succeeded)
            return getFoodResult.Error;
        
        // Should only happen right before return
        state.State.Foods.Clear();
        await state.WriteStateAsync();
        
        return mapper.Map<TakeFoodResponse>(getFoodResult.Value);
    }
}