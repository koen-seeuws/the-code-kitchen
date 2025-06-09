using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PantryController(IClusterClient clusterClient) : ControllerBase
{
    [HttpGet("{ingredient}/[action]")]
    public async Task<IActionResult> Take([FromRoute] string ingredient)
    {
        var cook = HttpContext.User.GetCookId();
        var request = new TakeFoodFromPantryRequest(ingredient, cook);
        var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
        var result = await pantryGrain.TakeFood(request);
        return this.MatchActionResult(result);
    }
}