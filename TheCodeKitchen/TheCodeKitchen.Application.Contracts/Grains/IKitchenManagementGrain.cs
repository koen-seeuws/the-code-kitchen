using Orleans;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IKitchenManagementGrain : IGrainWithGuidKey
{
    Task<Result<string>> GenerateUniqueCode(Guid kitchenId, int length = 4, string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
    Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request);
    Task<Result<TheCodeKitchenUnit>> DeleteKitchenCode(string code);
}