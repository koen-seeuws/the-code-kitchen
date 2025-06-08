using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public virtual async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request)
    {
        if(state.State.Foods.Count <= 0)
            return new EquipmentEmptyError($"The equipment {this.GetPrimaryKey()} does not contain an item");

        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        
        throw new NotImplementedException();
    }
}