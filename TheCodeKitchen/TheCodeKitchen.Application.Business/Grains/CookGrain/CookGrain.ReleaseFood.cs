using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<ReleaseFoodResponse>> ReleaseFood()
    {
        if (!state.RecordExists)
            return new NotFoundError($"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");

        if (!state.State.Food.HasValue)
            return new NotHoldingFoodError(
                $"The cook with name {state.State.Username} is not holding any food");
        
        var releaseFoodResponse = new ReleaseFoodResponse(state.State.Food.Value);

        state.State.Food = null;
        await state.WriteStateAsync();

        return releaseFoodResponse;
    }
}