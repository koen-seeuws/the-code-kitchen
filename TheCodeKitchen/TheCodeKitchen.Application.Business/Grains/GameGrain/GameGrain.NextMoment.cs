using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    private IGrainTimer? _nextMomentTimer;
    private TimeSpan? _nextMomentDelay;

    public async Task<Result<TheCodeKitchenUnit>> NextMoment()
    {
        var gameId = this.GetPrimaryKey();
        var moment = DateTimeOffset.Now;

        // Sending out event
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), gameId);
        var nextMomentEvent = new NextMomentEvent(state.State.Id, moment, state.State.Temperature, _nextMomentDelay);
        await stream.OnNextAsync(nextMomentEvent);

        // Order generation
        if (_timeUntilNewOrder is null)
        {
            await PickRandomTimeUntilNextOrder();
        }

        if (_timeUntilNewOrder >= TimeSpan.Zero)
            _timeUntilNewOrder -= TheCodeKitchenMomentDuration.Value;

        if (_timeUntilNewOrder <= TimeSpan.Zero)
        {
            await GenerateOrder();
        }

        // Keep grain active while game is playing
        await CheckAndDelayDeactivation();

        return TheCodeKitchenUnit.Value;
    }

    private Task CheckAndDelayDeactivation()
    {
        if (!_nextMomentDelay.HasValue) return Task.CompletedTask;
        var delay = _nextMomentDelay.Value.Add(TimeSpan.FromSeconds(30));
        DelayDeactivation(delay);
        return Task.CompletedTask;
    }
}