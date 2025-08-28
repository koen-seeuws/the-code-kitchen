using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class EquipmentGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        if (state.State.MixtureTime.HasValue)
            state.State.MixtureTime += TheCodeKitchenMomentDuration.Value;

        if (state.State.MixtureTemperature.HasValue)
        {
            state.State.MixtureTemperature = TemperatureHelper.CalculateNextMomentFoodTemperature(
                state.State.MixtureTemperature.Value,
                state.State.Temperature ?? nextMomentEvent.Temperature,
                state.State.TemperatureTransferRate ?? TheCodeKitchenRoomTemperatureTransferRate.Value
            );
        }
    }
}