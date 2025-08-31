namespace TheCodeKitchen.Application.Business.Helpers;

public static class TemperatureHelper
{
    public static double CalculateNextMomentFoodTemperature(double currentTemperature, double environmentTemperature, double temperatureTransferRate)
    {
        var seconds = TheCodeKitchenMomentDuration.Value.TotalSeconds;

        // Exponential approach to equilibrium
        var decayFactor = Math.Exp(-temperatureTransferRate * seconds);
        var newTemperature = environmentTemperature + (currentTemperature - environmentTemperature) * decayFactor;

        // Clamp to realistic bounds
        return Math.Clamp(newTemperature, -30, 400);
    }
}