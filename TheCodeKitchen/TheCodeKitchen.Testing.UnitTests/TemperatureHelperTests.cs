using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Testing.UnitTests;

public class TemperatureHelperTests
{
    [Theory]
    // 1. No temperature change (food = environment)
    [InlineData(10, 30, 50.0, 50.0, 0.05, 50.0)] // stays at 50
    [InlineData(10, 60, 50.0, 50.0, 0.05, 50.0)] // stays at 50
    [InlineData(5, 30, 200.0, 200.0, 0.10, 200.0)] // stays at 200
    [InlineData(5, 60, 200.0, 200.0, 0.10, 200.0)] // stays at 200

    // 2. Heating up (food colder than environment)
    [InlineData(1, 30, 20.0, 100.0, 0.05, 140.0)] // overshoots past 100 due to rate*seconds > 1
    [InlineData(1, 60, 20.0, 100.0, 0.05, 260.0)] // bigger overshoot with longer step
    [InlineData(5, 30, 20.0, 100.0, 0.05, 102.5)] // converges back near env after oscillations
    [InlineData(5, 60, 20.0, 100.0, 0.05, 360.0)] // overshoots high but clamped below 400
    [InlineData(20, 30, 20.0, 100.0, 0.01, 99.9)] // slowly converges very close to 100
    [InlineData(20, 60, 20.0, 100.0, 0.01, 100.0)] // reaches 100 exactly

    // 3. Cooling down (food hotter than environment)
    [InlineData(1, 30, 100.0, 20.0, 0.05, -20.0)] // overshoots below env
    [InlineData(1, 60, 100.0, 20.0, 0.05, -30.0)] // overshoot, clamped at -30
    [InlineData(5, 30, 100.0, 20.0, 0.05, 17.5)] // stabilizes near env after oscillations
    [InlineData(5, 60, 100.0, 20.0, 0.05, -30.0)] // repeated overshoot, clamped at -30
    [InlineData(20, 30, 100.0, 20.0, 0.01, 20.1)] // approaches 20 gradually
    [InlineData(20, 60, 100.0, 20.0, 0.01, 20.0)] // converges to 20

    // 4. Clamp at lower bound (-30 °C)
    [InlineData(10, 60, -10.0, -200.0, 0.10, -30.0)] // overshoot down, clamped
    [InlineData(10, 120, -10.0, -200.0, 0.10, -30.0)] // even bigger overshoot, clamped
    [InlineData(50, 120, 0.0, -1000.0, 0.20, -30.0)] // way below, clamped
    [InlineData(50, 240, 0.0, -1000.0, 0.20, -30.0)] // way below, clamped

    // 5. Clamp at upper bound (400 °C)
    [InlineData(10, 60, 300.0, 1000.0, 0.10, 400.0)] // overshoot up, clamped
    [InlineData(10, 120, 300.0, 1000.0, 0.10, 400.0)] // even bigger overshoot, clamped
    [InlineData(50, 120, 100.0, 2000.0, 0.20, 400.0)] // way above, clamped
    [InlineData(50, 240, 100.0, 2000.0, 0.20, 400.0)] // way above, clamped

    // 6. No transfer (rate = 0)
    [InlineData(10, 30, 50.0, 200.0, 0.0, 50.0)] // no change
    [InlineData(10, 60, 50.0, 200.0, 0.0, 50.0)] // no change
    [InlineData(10, 30, 300.0, 0.0, 0.0, 300.0)] // no change
    [InlineData(10, 60, 300.0, 0.0, 0.0, 300.0)] // no change

    // 7. Long convergence (approaching environment asymptotically)
    [InlineData(100, 30, 20.0, 100.0, 0.05, 100.0)] // stabilizes at 100 after many steps
    [InlineData(100, 60, 20.0, 100.0, 0.05, -30.0)] // oscillates and overshoots, ends clamped
    [InlineData(100, 30, 200.0, 20.0, 0.05, 20.0)] // stabilizes at 20 after many steps
    [InlineData(100, 60, 200.0, 20.0, 0.05, 120.0)] // oscillates, ends at 120
    public void ShouldCorrectlyCalculateNextMomentTemperature(
        int nextMomentsPassed, int nextMomentDurationInSeconds, double currentTemperature,
        double environmentTemperature, double transferRate, double expectedNextTemperature
    )
    {
        //Arrange
        const int decimalPrecision = 1;
        TheCodeKitchenMomentDuration.Value = TimeSpan.FromSeconds(nextMomentDurationInSeconds);

        // Act
        for (var i = 0; i < nextMomentsPassed; i++)
        {
            currentTemperature = TemperatureHelper
                .CalculateNextMomentFoodTemperature(currentTemperature, environmentTemperature, transferRate);
        }

        // Assert
        Assert.Equal(expectedNextTemperature, currentTemperature,
            decimalPrecision); // Allowing a precision of 1 decimal place
    }
}