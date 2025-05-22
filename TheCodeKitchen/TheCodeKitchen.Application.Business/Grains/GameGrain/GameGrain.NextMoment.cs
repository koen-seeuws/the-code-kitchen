using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    private IGrainTimer? _nextMomentTimer;
    private TimeSpan? _nextMomentDelay;

    private async Task NextMoment()
    {
        var gameId = this.GetPrimaryKey();
        var now = DateTimeOffset.Now;

        logger.LogInformation("Logging time from GameGrain {id}: {time} (x{modifier})",
            gameId, now, state.State.SpeedModifier);

        // Sending out event
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.AzureStorageQueuesProvider);
        var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), gameId);
        var nextMomentEvent = new NextMomentEvent(state.State.Id, now);
        await stream.OnNextAsync(nextMomentEvent);

        // Order generation
        if (--_secondsUntilNewOrder <= 0)
        {
            await GenerateOrder();
            await PickSecondsUntilNextOrder();
        }

        // Keep grain active while game is playing
        await CheckAndDelayDeactivation();
    }

    private Task CheckAndDelayDeactivation()
    {
        if (!_nextMomentDelay.HasValue) return Task.CompletedTask;
        var delay = _nextMomentDelay.Value.Add(TimeSpan.FromSeconds(30));
        DelayDeactivation(delay);
        return Task.CompletedTask;
    }
}