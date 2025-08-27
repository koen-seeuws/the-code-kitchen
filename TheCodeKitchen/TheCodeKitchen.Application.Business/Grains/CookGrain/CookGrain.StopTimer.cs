using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> StopTimer(StopTimerRequest request)
    {
        if (!state.RecordExists)
        {
            logger.LogWarning("The cook with username {Username} does not exist in kitchen {Kitchen}",
                this.GetPrimaryKeyString(), this.GetPrimaryKey());

            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        }

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Stopping timer number {TimerNumber}...",
            state.State.Kitchen, state.State.Username, request.Number);

        var timer = state.State.Timers.FirstOrDefault(t => t.Number == request.Number);

        if (timer is null)
        {
            logger.LogWarning(
                "Kitchen {Kitchen} - Cook {Username}: No timer with number {TimerNumber} set by this cook",
                state.State.Kitchen, state.State.Username, request.Number);
            return new NotFoundError($"You no longer have a timer with number {request.Number} addressed to you");
        }
        
        state.State.Timers.Remove(timer);

        await state.WriteStateAsync();
        
        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Timer with number {TimerNumber} stopped successfully",
            state.State.Kitchen, state.State.Username, request.Number);

        return new TheCodeKitchenUnit();
    }
}