using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private async Task GenerateOrder()
    {
        //TODO: Implement order generation logic
        logger.LogInformation("Generating order...");
    }
}