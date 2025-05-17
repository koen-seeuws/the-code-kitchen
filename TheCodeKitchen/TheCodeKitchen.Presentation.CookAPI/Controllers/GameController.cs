using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Infrastructure.Security;
using TheCodeKitchen.Presentation;

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
    public async Task<IActionResult> Join([FromBody] AuthenticationRequest request)
    {
        var kitchenCodeIndexGrain = client.GetGrain<IKitchenManagementGrain>(Guid.Empty);

        var passwordHash = passwordHashingService.HashPassword(request.Password);
        
        var joinKitchenRequest = new JoinKitchenRequest(
            request.Username,
            passwordHash,
            request.KitchenCode
        );

        var joinKitchenResult = await kitchenCodeIndexGrain.JoinKitchen(joinKitchenRequest);

        if (!joinKitchenResult.Succeeded)
            return this.MatchActionResult(joinKitchenResult);

        if (
            !joinKitchenResult.Value.isNewCook &&
            !passwordHashingService.VerifyHashedPassword(joinKitchenResult.Value.PasswordHash, request.Password)
        )
            return this.MatchActionResult(new UnauthorizedError("Invalid password"));

        var token = securityTokenService.GeneratePlayerToken(
            joinKitchenResult.Value.CookId,
            joinKitchenResult.Value.Username,
            joinKitchenResult.Value.KitchenId
        );

        return Ok(new AuthenticationResponse(token));
    }
}