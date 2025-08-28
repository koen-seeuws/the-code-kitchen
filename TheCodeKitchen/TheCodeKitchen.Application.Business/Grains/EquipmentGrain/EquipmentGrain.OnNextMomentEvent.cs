using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class EquipmentGrain
{
    private Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        if (state.State.MixtureTime.HasValue)
            state.State.MixtureTime += TheCodeKitchenMomentDuration.Value;

        foreach (var food in state.State.Foods)
        {
            food.Temperature = TemperatureHelper.CalculateNextMomentFoodTemperature(
                food.Temperature,
                state.State.Temperature ?? nextMomentEvent.Temperature,
                state.State.TemperatureTransferRate ?? TheCodeKitchenRoomTemperatureTransferRate.Value
            );
        }

        return Task.CompletedTask;
    }
}