using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Contracts.Events.Cook;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        if (state.State.Food is not null)
            state.State.Food.Temperature = TemperatureHelper.CalculateNextMomentFoodTemperature(
                nextMomentEvent.TimePerMoment,
                state.State.Food.Temperature,
                nextMomentEvent.Temperature,
                RoomTemperatureTransferRate.Value
            );

        var timerElapsedTasks = new List<Task>();
        
        foreach (var timer in state.State.Timers)
        {
            timer.Time -= nextMomentEvent.TimePerMoment;
            
            if (timer.Time > TimeSpan.Zero) continue;
            
            timer.Time = TimeSpan.Zero;
            var @event = new TimerElapsedEvent(timer.Number, timer.Note);
            var timerElapsedTask = realTimeCookService.SendTimerElapsedEvent(state.State.Username, @event);
            timerElapsedTasks.Add(timerElapsedTask);

        }

        await Task.WhenAll(timerElapsedTasks);
    }
}