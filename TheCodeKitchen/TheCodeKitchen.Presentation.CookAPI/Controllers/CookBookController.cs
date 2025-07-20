using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[Tags("Cook Book")]
[ApiController]
[Route("[controller]")]
[Authorize]
public class CookBookController(IClusterClient clusterClient) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Read()
    {
        var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);
        var result = await cookBookGrain.GetRecipes();
        return this.MatchActionResult(result);
    }
}