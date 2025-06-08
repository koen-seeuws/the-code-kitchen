using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Presentation.ManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PantryController(IClusterClient clusterClient) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetIngredients()
    {
        var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
        var result = await pantryGrain.GetIngredients();
        return this.MatchActionResult(result);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientRequest createIngredientRequest)
    {
        var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
        var result = await pantryGrain.CreateIngredient(createIngredientRequest);
        return this.MatchActionResult(result);
    }
}