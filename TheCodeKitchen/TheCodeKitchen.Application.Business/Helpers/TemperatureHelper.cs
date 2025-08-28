using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Application.Business.Helpers;

public static class TemperatureHelper
{
    public static double CalculateNextMomentFoodTemperature(double currentFoodTemperature, double equipmentTemperature, double temperatureTransferRate)
    {
        var seconds = TheCodeKitchenMomentDuration.Value.TotalSeconds;

        // Apply time-based temperature change
        var temperatureDelta = (equipmentTemperature - currentFoodTemperature)
                               * temperatureTransferRate
                               * seconds;

        currentFoodTemperature += temperatureDelta;

        // Clamp to realistic bounds
        return Math.Clamp(currentFoodTemperature, -30, 400);
    }
}