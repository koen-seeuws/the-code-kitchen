using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Infrastructure.Security;
using TheCodeKitchen.Presentation.WebCore;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(
    IClusterClient client,
    IPasswordHashingService passwordHashingService,
    ISecurityTokenService securityTokenService
) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Join([FromBody] JoinKitchenRequest request)
    {
        var kitchenCodeIndexGrain = client.GetGrain<IKitchenCodeIndexGrain>(Guid.Empty);
        var kitchenIdResult = await kitchenCodeIndexGrain.GetKitchenId(request.KitchenCode);

        if (!kitchenIdResult.Succeeded)
            return this.MatchActionResult(kitchenIdResult);

        var hashedPassword = passwordHashingService.HashPassword(request.Password);
        request = request with { Password = hashedPassword };

        var kitchenGrain = client.GetGrain<IKitchenGrain>(kitchenIdResult.Value);
        var result = await kitchenGrain.JoinKitchen(request);

        return this.MatchActionResult(result);
    }
}