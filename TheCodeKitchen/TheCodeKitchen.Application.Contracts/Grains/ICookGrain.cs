using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface ICookGrain : IGrainWithGuidKey
{
    Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request);
    Task<Result<GetCookResponse>> GetCook();
}
