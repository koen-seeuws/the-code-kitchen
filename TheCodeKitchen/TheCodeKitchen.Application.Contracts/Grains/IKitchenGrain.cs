using Orleans;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IKitchenGrain : IGrainWithGuidKey
{
    Task<Result<CreateKitchenResponse>> Initialize(CreateKitchenRequest request, int count);
    Task<Result<GetKitchenResponse>> GetKitchen();
}