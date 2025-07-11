using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public Task<Result<GetFoodResponse>> GetFood()
    {
        Result<GetFoodResponse> result = state.RecordExists
            ? mapper.Map<GetFoodResponse>(state.State)
            : new NotFoundError($"The food with id {this.GetPrimaryKey()} does not exist");
        return Task.FromResult(result);
    }
}