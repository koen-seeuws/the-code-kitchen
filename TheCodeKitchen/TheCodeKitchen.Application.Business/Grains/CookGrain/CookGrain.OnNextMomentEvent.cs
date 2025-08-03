using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var timerStreamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var timerFinishedStream = timerStreamProvider.GetStream<TimerFinishedEvent>(nameof(TimerFinishedEvent),
            $"{state.State.Kitchen}+{state.State.Username}");

        var timerFinishedTasks = new List<Task>();

        foreach (var timer in state.State.Timers)
        {
            if (timer.Time > TimeSpan.Zero)
            {
                timer.Time -= TheCodeKitchenMomentDuration.Value;
            }
            else
            {
                var timerFinishedEvent = new TimerFinishedEvent(timer.Number, timer.Note);
                timerFinishedTasks.Add(timerFinishedStream.OnNextAsync(timerFinishedEvent));
            }
        }

        await Task.WhenAll(timerFinishedTasks);
    }
}