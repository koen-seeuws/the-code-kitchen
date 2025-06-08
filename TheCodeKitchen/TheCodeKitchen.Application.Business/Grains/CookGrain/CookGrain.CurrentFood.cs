using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public Task<Result<CurrentFoodResponse>> CurrentFood()
    {
        Result<CurrentFoodResponse> result = state.RecordExists
            ? new CurrentFoodResponse(state.State.Food)
            : new NotFoundError($"The cook with id {this.GetPrimaryKey()} does not exist");
        
        return Task.FromResult(result);
    }
}