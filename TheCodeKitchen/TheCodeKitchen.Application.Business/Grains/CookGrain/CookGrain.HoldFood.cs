using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> HoldFood(HoldFoodRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The cook with id {this.GetPrimaryKey()} does not exist");

        if (state.State.Food != null)
            return new AlreadyHoldingFoodError(
                $"The cook with name {state.State.Username} is already holding food with id {state.State.Food}");

        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(request.FoodId);
        var setCookRequest = new SetCookRequest(state.State.Id);
        var setCookResult = await foodGrain.SetCook(setCookRequest);
        
        if(!setCookResult.Succeeded)
            return setCookResult.Error;
        
        state.State.Food = request.FoodId;
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}