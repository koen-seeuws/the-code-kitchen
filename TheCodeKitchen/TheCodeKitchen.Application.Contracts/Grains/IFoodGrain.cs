using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IFoodGrain : IGrainWithGuidKey
{
    Task<Result<CreateFoodResponse>> Initialize(CreateFoodRequest request);
}