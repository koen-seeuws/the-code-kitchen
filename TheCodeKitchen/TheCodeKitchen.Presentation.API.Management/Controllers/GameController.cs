using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest createGameRequest)
    {
        var request = mapper.Map<CreateGameCommand>(createGameRequest);
        var result = await mediator.Send(request);
        return this.MatchActionResult(result);
    }
}