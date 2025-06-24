using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public virtual async Task<Result<TheCodeKitchenUnit>> AddFood(AddFoodRequest request)
    {
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var releaseFoodResult = await cookGrain.ReleaseFood();

        if (!releaseFoodResult.Succeeded)
            return releaseFoodResult.Error;
        
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(releaseFoodResult.Value.Food);
        var setEquipmentRequest =
            new SetEquipmentRequest(state.State.EquipmentType, state.State.Number);
        
        var setEquipmentResult = await foodGrain.SetEquipment(setEquipmentRequest);
        
        if (!setEquipmentResult.Succeeded)
            return setEquipmentResult.Error;
        
        state.State.Foods.Add(releaseFoodResult.Value.Food);
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}