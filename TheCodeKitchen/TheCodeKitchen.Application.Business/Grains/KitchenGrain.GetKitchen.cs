using Orleans;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenGrain
{
    public Task<Result<GetKitchenResponse>> GetKitchen()
    {
        Result<GetKitchenResponse> result = state.RecordExists
            ? mapper.Map<GetKitchenResponse>(state.State)
            : new NotFoundError($"The kitchen with id {this.GetPrimaryKey()} was not found");

        return Task.FromResult(result);
    }
}