using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Join([FromBody] JoinGameRequest request)
    {
        var command = mapper.Map<JoinGameCommand>(request);
        var result = await mediator.Send(command);
        return this.MatchActionResult(result);
    }
}