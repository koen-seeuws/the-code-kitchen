using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.ManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IClusterClient client) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest createGameRequest)
    {
        var gameManagementGrain = client.GetGrain<IGameManagementGrain>(Guid.Empty);
        var result = await gameManagementGrain.CreateGame(createGameRequest);
        return this.MatchActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var gameManagementGrain = client.GetGrain<IGameManagementGrain>(Guid.Empty);
        var result = await gameManagementGrain.GetGames();
        return this.MatchActionResult(result);
    }
    
    [HttpGet("{gameId}/[action]")]
    public async Task<IActionResult> Get(Guid gameId)
    {
        var gameGrain = client.GetGrain<IGameGrain>(gameId);
        var result = await gameGrain.GetGame();
        return this.MatchActionResult(result);
    }

    [HttpPut("{gameId}/[action]")]
    public async Task<IActionResult> Start(Guid gameId)
    {
        var gameGrain = client.GetGrain<IGameGrain>(gameId);
        var result = await gameGrain.StartGame();
        return this.MatchActionResult(result);
    }

    [HttpPut("{gameId}/[action]")]
    public async Task<IActionResult> PauseOrUnpause(Guid gameId)
    {
        var gameGrain = client.GetGrain<IGameGrain>(gameId);
        var result = await gameGrain.PauseOrUnpauseGame();
        return this.MatchActionResult(result);
    }
    
    [HttpPatch("{gameId}/[action]")]
    public async Task<IActionResult> ModifySpeed(Guid gameId, [FromBody] float speedModifier)
    {
        var request = new UpdateGameRequest(speedModifier);
        var gameGrain = client.GetGrain<IGameGrain>(gameId);
        var result = await gameGrain.UpdateGame(request);
        return this.MatchActionResult(result);
    }
}