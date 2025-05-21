namespace TheCodeKitchen.Application.Contracts.Grains;

public interface ICookGrain : IGrainWithGuidKey
{
    Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request);
    Task<Result<GetCookResponse>> GetCook();
}
