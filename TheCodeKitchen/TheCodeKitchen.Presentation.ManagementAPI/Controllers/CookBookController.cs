using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;

namespace TheCodeKitchen.Presentation.ManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CookBookController(IClusterClient clusterClient) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetRecipes()
    {
        var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);
        var result = await cookBookGrain.GetRecipes();
        return this.MatchActionResult(result);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest createRecipeRequest)
    {
        var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);
        var result = await cookBookGrain.CreateRecipe(createRecipeRequest);
        return this.MatchActionResult(result);
    }
}