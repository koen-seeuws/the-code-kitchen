using Orleans;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface IKitchenCodeIndexGrain : IGrainWithGuidKey
{
    Task<Result<string>> GenerateUniqueCode(Guid kitchenId, int length = 4, string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
    Task<Result<Guid>> GetKitchenId(string code);
    Task<Result<TheCodeKitchenUnit>> DeleteKitchenCode(string code);
}