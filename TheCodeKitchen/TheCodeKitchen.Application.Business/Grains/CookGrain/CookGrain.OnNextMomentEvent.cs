using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var timerElapsedTasks = new List<Task>();

        foreach (var timer in state.State.Timers)
        {
            if (timer.Time > TimeSpan.Zero)
            {
                timer.Time -= TheCodeKitchenMomentDuration.Value;
            }
            else
            {
                var timerElapsedEvent = new TimerElapsedEvent(timer.Number, timer.Note);
                var sendTimerElapsedTask =
                    realTimeCookService.SendTimerElapsedEvent(state.State.Username, timerElapsedEvent);
                timerElapsedTasks.Add(sendTimerElapsedTask);
            }
        }

        await Task.WhenAll(timerElapsedTasks);
    }
}