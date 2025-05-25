using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Contracts.Grains.Equipment;

public interface IEquipmentGrain
{
    Task<Result<CreateEquipmentResponse>> Initialize(CreateEquipmentRequest request);
    Task<Result<TheCodeKitchenUnit>> AddItem(AddItemRequest request);
    Task<Result<GetItemResponse>> GetItem();
}