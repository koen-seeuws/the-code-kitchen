using Orleans;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class CookGrain
{
    public Task<Result<GetCookResponse>> GetCook()
    {
        Result<GetCookResponse> result = state.RecordExists
            ? mapper.Map<GetCookResponse>(state.State)
            : new NotFoundError($"The cook with id {this.GetPrimaryKey()} does not exist");
        return Task.FromResult(result);
    }
}