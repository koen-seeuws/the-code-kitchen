using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Contracts.Grains.Equipment;

public interface IEquipmentGrain
{
    Task<Result<CreateEquipmentResponse>> Initialize(CreateEquipmentRequest request);
    Task<Result<TheCodeKitchenUnit>> AddFood(AddFoodRequest request);
    Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request);
}