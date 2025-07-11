using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IFoodGrain : IGrainWithGuidKey
{
    Task<Result<CreateFoodResponse>> Initialize(CreateFoodRequest request);
    Task<Result<GetFoodResponse>> GetFood();
    Task<Result<TheCodeKitchenUnit>> SetCook(SetCookRequest request);
    Task<Result<TheCodeKitchenUnit>> SetEquipment(SetEquipmentRequest request);
    Task<Result<TheCodeKitchenUnit>> SetOrder(SetOrderRequest request);
    Task<Result<TheCodeKitchenUnit>> Trash();
}