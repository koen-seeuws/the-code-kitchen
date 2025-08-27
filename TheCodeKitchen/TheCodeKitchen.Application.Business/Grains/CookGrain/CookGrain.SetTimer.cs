using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using Timer = TheCodeKitchen.Core.Domain.Timer;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetTimer(SetTimerRequest request)
    {
        if (!state.RecordExists)
        {
            logger.LogWarning("The cook with username {Username} does not exist in kitchen {Kitchen}",
                this.GetPrimaryKeyString(), this.GetPrimaryKey());

            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        }
        
        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Setting timer...",
            state.State.Kitchen, state.State.Username);

        var number = state.State.Timers.Count + 1;
        var timer = new Timer(number, request.Time, request.Note);
        state.State.Timers.Add(timer);
        
        await state.WriteStateAsync();
        
        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Timer set with number {TimerNumber}",
            state.State.Kitchen, state.State.Username, number);

        return TheCodeKitchenUnit.Value;
    }
}
    
