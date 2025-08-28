using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Cook;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var timerElapsedTasks = new List<Task>();

        foreach (var timer in state.State.Timers)
        {
            if (timer.Time > TimeSpan.Zero)
                timer.Time -= TheCodeKitchenMomentDuration.Value;

            if (timer.Time > TimeSpan.Zero)
                continue;

            var @event = new TimerElapsedEvent(timer.Number, timer.Note);
            var sendTimerElapsedTask = realTimeCookService.SendTimerElapsedEvent(state.State.Username, @event);
            timerElapsedTasks.Add(sendTimerElapsedTask);
        }

        await Task.WhenAll(timerElapsedTasks);

        if (state.State.Food is not null)
            state.State.Food.Temperature = TemperatureHelper.CalculateNextMomentFoodTemperature(
                state.State.Food.Temperature, nextMomentEvent.Temperature,
                TheCodeKitchenRoomTemperatureTransferRate.Value
            );
    }
}