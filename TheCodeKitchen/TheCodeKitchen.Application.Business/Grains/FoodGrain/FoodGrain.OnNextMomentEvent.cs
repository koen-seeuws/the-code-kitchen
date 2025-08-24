using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public sealed partial class FoodGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        // Get both transfer rate and equipment temperature based on equipment type
        var (temperatureTransferRate, equipmentTemperature) = state.State.CurrentEquipmentType switch
        {
            EquipmentType.Bbq => (0.03, 270),
            EquipmentType.Fridge => (0.005, 3),
            EquipmentType.Freezer => (0.003, -18),
            EquipmentType.Fryer => (0.04, 180),
            EquipmentType.HotPlate => (0.025, nextMomentEvent.Temperature + 15),
            EquipmentType.Oven => (0.015, 225),
            EquipmentType.Stove => (0.03, 225),
            _ => (0.02, nextMomentEvent.Temperature) // Room temperature
        };

        var currentFoodTemperature = state.State.Temperature;
        var seconds = TheCodeKitchenMomentDuration.Value.TotalSeconds;

        // Apply time-based temperature change
        var temperatureDelta = (equipmentTemperature - currentFoodTemperature)
                               * temperatureTransferRate
                               * seconds;

        state.State.Temperature += temperatureDelta;

        // Optional: Clamp to realistic bounds
        state.State.Temperature = Math.Clamp(state.State.Temperature, -30, 400);
    }
}
