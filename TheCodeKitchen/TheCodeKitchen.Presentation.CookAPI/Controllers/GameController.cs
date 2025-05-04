using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Interfaces.Security;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Infrastructure.Security;
using TheCodeKitchen.Presentation.API.Cook.Models;
using TheCodeKitchen.Presentation.WebCore;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(
    IClusterClient client,
    ISecurityTokenService securityTokenService
) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Join([FromBody] JoinKitchenRequest request)
    {
        var kitchenCodeIndexGrain = client.GetGrain<IKitchenManagementGrain>(Guid.Empty);

        var joinKitchenResult = await kitchenCodeIndexGrain.JoinKitchen(request);

        if (!joinKitchenResult.Succeeded)
            return this.MatchActionResult(joinKitchenResult);

        var token = securityTokenService.GeneratePlayerToken(
            joinKitchenResult.Value.CookId,
            joinKitchenResult.Value.Username,
            joinKitchenResult.Value.KitchenId
        );

        return Ok(new AuthenticationResponse(token));
    }
}