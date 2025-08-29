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
                state.State.Food.Temperature, nextMomentEvent.Temperature,
                TheCodeKitchenRoomTemperatureTransferRate.Value
            );

        var nonElapsedTimers = state.State.Timers
            .Where(timer => timer.Time > TimeSpan.Zero)
            .ToArray();

        foreach (var timer in nonElapsedTimers)
        {
            timer.Time -= TheCodeKitchenMomentDuration.Value;
        }

        var timerElapsedTasks = state.State.Timers
            .Except(nonElapsedTimers)
            .Select(t =>
            {
                var @event = new TimerElapsedEvent(t.Number, t.Note);
                return realTimeCookService.SendTimerElapsedEvent(state.State.Username, @event);
            })
            .ToArray();

        await Task.WhenAll(timerElapsedTasks);
    }
}