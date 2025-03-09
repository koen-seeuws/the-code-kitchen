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
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateGameRequest createGameRequest,
        CancellationToken cancellationToken = default
    )
    {
        var request = mapper.Map<CreateGameCommand>(createGameRequest);
        var result = await mediator.Send(request, cancellationToken);
        return this.MatchActionResult(result);
    }
}