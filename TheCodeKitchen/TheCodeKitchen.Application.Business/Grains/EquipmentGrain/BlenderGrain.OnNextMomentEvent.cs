using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class BlenderGrain
{
    protected override Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        //TODO
        logger.LogInformation("Blender {gameId} {kitchenId} {number}: OnNextMomentEvent {moment}", 
            _state.State.Game, _state.State.Kitchen, _state.State.Number, nextMomentEvent.Moment);
        return Task.CompletedTask;
    }
}