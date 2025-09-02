using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;
using TheCodeKitchen.Application.Validation;
using TheCodeKitchen.Application.Validation.Pantry;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public sealed class PantryController(
    IClusterClient clusterClient,
    TakeFoodFromPantryValidator foodFromPantryValidator
) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Inventory()
    {
        var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
        var result = await pantryGrain.GetIngredients();
        return this.MatchActionResult(result);
    }

    [HttpPost("{ingredient}/[action]")]
    public async Task<IActionResult> TakeFood([FromRoute] string ingredient)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var cook = HttpContext.User.GetUsername();
        var request = new TakeFoodFromPantryRequest(ingredient, kitchen, cook);
        if (!foodFromPantryValidator.ValidateAndError(request, out var error)) return this.MatchActionResult(error);
        var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
        var result = await pantryGrain.TakeFood(request);
        return this.MatchActionResult(result);
    }
}