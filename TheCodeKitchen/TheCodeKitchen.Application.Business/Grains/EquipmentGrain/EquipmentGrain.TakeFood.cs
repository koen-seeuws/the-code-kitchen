using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request)
    {
        if(state.State.Foods.Count <= 0)
            return new EquipmentEmptyError($"The equipment {this.GetPrimaryKey()} does not contain an item");

        return new TakeFoodResponse(Guid.Empty);
        
        
        //TODO: food merging
        var todoFoodId = Guid.NewGuid();

        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var holdFoodRequest = new HoldFoodRequest(todoFoodId);
        var holdFoodResult = await cookGrain.HoldFood(holdFoodRequest);
        
        
        
        throw new NotImplementedException();
    }
}