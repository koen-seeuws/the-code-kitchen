using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

//Key exists of OrderNumber and GameId
public interface IOrderGrain : IGrainWithIntegerCompoundKey
{
    Task<Result<CreateOrderResponse>> Initialize(CreateOrderRequest request);
}