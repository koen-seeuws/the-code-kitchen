using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public Task<Result<CurrentFoodResponse>> CurrentFood()
    {
        Result<CurrentFoodResponse> result = state.RecordExists
            ? new CurrentFoodResponse(state.State.Food)
            : new NotFoundError($"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        
        return Task.FromResult(result);
    }
}