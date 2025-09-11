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
            // Skip timers that are already at zero
            if (timer.Time == TimeSpan.Zero) continue;
    
            var previousTime = timer.Time;
            timer.Time -= nextMomentEvent.TimePerMoment;

            // Fire event if it was positive and now is zero or negative
            if (previousTime > TimeSpan.Zero && timer.Time <= TimeSpan.Zero)
            {
                timer.Time = TimeSpan.Zero; // clamp at zero
                var @event = new TimerElapsedEvent(timer.Number, timer.Note);
                var timerElapsedTask = realTimeCookService.SendTimerElapsedEvent(state.State.Username, @event);
                timerElapsedTasks.Add(timerElapsedTask);
            }
        }

        await Task.WhenAll(timerElapsedTasks);
    }
}