using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Queries;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateGameRequest createGameRequest,
        CancellationToken cancellationToken = default
    )
    {
        var command = mapper.Map<CreateGameCommand>(createGameRequest);
        var result = await mediator.Send(command, cancellationToken);
        return this.MatchActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var query = new GetGamesQuery();
        var result = await mediator.Send(query, cancellationToken);
        return this.MatchActionResult(result);
    }

    [HttpPut("{gameId}/[action]")]
    public async Task<IActionResult> Start(CancellationToken cancellationToken = default)
    {
        /*
        var command = mapper.Map<Sta>(createGameRequest);
        var result = await mediator.Send(command, cancellationToken);
        return this.MatchActionResult(result);
        */
        return BadRequest();
    }
}