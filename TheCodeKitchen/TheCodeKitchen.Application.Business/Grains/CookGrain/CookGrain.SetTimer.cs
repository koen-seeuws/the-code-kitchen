using TheCodeKitchen.Application.Contracts.Requests.Cook;
using Timer = TheCodeKitchen.Core.Domain.Timer;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetTimer(SetTimerRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");

        var number = state.State.Timers.Count + 1;
        var timer = new Timer(number, request.Time, request.Note);
        state.State.Timers.Add(timer);
        
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}
    
