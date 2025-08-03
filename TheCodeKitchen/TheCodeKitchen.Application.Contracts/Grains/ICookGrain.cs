using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Response.Cook;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface ICookGrain : IGrainWithGuidCompoundKey
{
    Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request);
    Task<Result<GetCookResponse>> GetCook();
    Task<Result<GetKitchenResponse>> GetKitchen();
    Task<Result<TheCodeKitchenUnit>> HoldFood(HoldFoodRequest request); 
    Task<Result<ReleaseFoodResponse>> ReleaseFood();
    Task<Result<CurrentFoodResponse>> CurrentFood();
    Task<Result<TheCodeKitchenUnit>> DeliverMessage(DeliverMessageToCookRequest toCookRequest);
    Task<Result<IEnumerable<ReadMessageResponse>>> ReadMessages();
    Task<Result<TheCodeKitchenUnit>> ConfirmMessage(ConfirmMessageRequest request);
}
