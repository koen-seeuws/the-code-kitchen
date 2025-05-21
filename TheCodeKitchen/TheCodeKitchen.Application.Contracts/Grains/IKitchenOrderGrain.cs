namespace TheCodeKitchen.Application.Contracts.Grains;

//Key exists of OrderNumber and KitchenId
public interface IKitchenOrderGrain : IGrainWithIntegerCompoundKey
{
    Task<Result<CreateKitchenOrderResponse>> Initialize(CreateKitchenOrderRequest request);
}