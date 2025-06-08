using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Grains;

//Key exists of OrderNumber and KitchenId
public interface IKitchenOrderGrain : IGrainWithIntegerCompoundKey
{
    Task<Result<CreateKitchenOrderResponse>> Initialize(CreateKitchenOrderRequest request);
}