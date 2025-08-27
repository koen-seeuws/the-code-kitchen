using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<IEnumerable<GetTimerResponse>>> GetTimers()
    {
        if (!state.RecordExists)
        {
            logger.LogWarning("The cook with username {Username} does not exist in kitchen {Kitchen}",
                this.GetPrimaryKeyString(), this.GetPrimaryKey());
            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        }

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Getting timers...",
            state.State.Kitchen, state.State.Username);

        return mapper.Map<List<GetTimerResponse>>(state.State.Timers);
    }
}