using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ReleaseFood(ReleaseFoodRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The cook with id {this.GetPrimaryKey()} does not exist");

        if (state.State.Food == null)
            return new NotHoldingFoodError(
                $"The cook with id {this.GetPrimaryKey()} is not holding any food");

        state.State.Food = null;
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}