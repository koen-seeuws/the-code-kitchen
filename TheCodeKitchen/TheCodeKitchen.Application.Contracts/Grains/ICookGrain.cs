using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Response.Cook;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface ICookGrain : IGrainWithGuidKey
{
    Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request);
    Task<Result<GetCookResponse>> GetCook();
    Task<Result<GetKitchenResponse>> GetKitchen();
    Task<Result<TheCodeKitchenUnit>> HoldFood(HoldFoodRequest request); 
    Task<Result<TheCodeKitchenUnit>> ReleaseFood(ReleaseFoodRequest request);
    Task<Result<CurrentFoodResponse>> CurrentFood();
}
