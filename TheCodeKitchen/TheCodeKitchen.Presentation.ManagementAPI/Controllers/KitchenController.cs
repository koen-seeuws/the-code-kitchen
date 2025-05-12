using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Presentation.WebCore;

namespace TheCodeKitchen.Presentation.ManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class KitchenController(IClusterClient client) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKitchenRequest createKitchenRequest, CancellationToken cancellationToken = default)
    {
        var gameGrain = client.GetGrain<IGameGrain>(createKitchenRequest.GameId);
        var result = await gameGrain.CreateKitchen(createKitchenRequest);
        return this.MatchActionResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetKitchens([FromQuery] Guid gameId, CancellationToken cancellationToken = default)
    {
        var gameGrain = client.GetGrain<IGameGrain>(gameId);
        var result = await gameGrain.GetKitchens();
        return this.MatchActionResult(result);
    }
}