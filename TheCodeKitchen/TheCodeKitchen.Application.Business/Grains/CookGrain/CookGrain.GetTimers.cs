using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public Task<Result<IEnumerable<GetTimerResponse>>> GetTimers()
    {
        Result<IEnumerable<GetTimerResponse>> result = state.RecordExists
            ? mapper.Map<List<GetTimerResponse>>(state.State.Timers)
            : new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        return Task.FromResult(result);
    }
}