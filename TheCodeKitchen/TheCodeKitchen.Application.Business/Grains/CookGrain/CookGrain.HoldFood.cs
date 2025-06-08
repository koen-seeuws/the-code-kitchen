using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> HoldFood(HoldFoodRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The cook with id {this.GetPrimaryKey()} does not exist");

        if (state.State.Food != null)
            return new AlreadyHoldingFoodError(
                $"The cook with id {this.GetPrimaryKey()} is already holding food with id {state.State.Food}");

        state.State.Food = request.FoodId;
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}