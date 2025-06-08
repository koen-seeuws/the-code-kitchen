using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Application.Contracts.Response.Cook;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IKitchenGrain : IGrainWithGuidKey
{
    Task<Result<CreateKitchenResponse>> Initialize(CreateKitchenRequest request, int count);
    Task<Result<GetKitchenResponse>> GetKitchen();    
    Task<Result<IEnumerable<GetCookResponse>>> GetCooks(GetCookRequest request);
    Task<Result<TheCodeKitchenUnit>> ReleaseJoinCode();
    Task<Result<CreateCookResponse>> CreateCook(CreateCookRequest request);
    Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request);
    
}