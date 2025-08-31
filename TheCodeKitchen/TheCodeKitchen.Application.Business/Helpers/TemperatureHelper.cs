namespace TheCodeKitchen.Application.Business.Helpers;

public static class TemperatureHelper
{
    public static double CalculateNextMomentFoodTemperature(double currentTemperature, double environmentTemperature, double temperatureTransferRate)
    {
        var seconds = TheCodeKitchenMomentDuration.Value.TotalSeconds;

        // Apply time-based temperature change
        var temperatureDelta = (environmentTemperature - currentTemperature)
                               * temperatureTransferRate
                               * seconds;

        currentTemperature += temperatureDelta;

        // Clamp to realistic bounds
        return Math.Clamp(currentTemperature, -30, 400);
    }
}