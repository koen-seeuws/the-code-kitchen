using TheCodeKitchen.Application.Contracts.Requests.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public virtual async Task<Result<TheCodeKitchenUnit>> AddFood(AddFoodRequest request)
    {
        if(state.State.Foods.Count >= maxItems)
            return new EquipmentFullError($"The equipment {this.GetPrimaryKey()} is full");
        
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        
        
        // TODO: Possibly check something
        
        
        
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}