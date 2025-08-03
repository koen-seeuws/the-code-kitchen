using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public Task<Result<GetCookResponse>> GetCook()
    {
        Result<GetCookResponse> result = state.RecordExists
            ? mapper.Map<GetCookResponse>(state.State)
            : new NotFoundError($"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        return Task.FromResult(result);
    }
}