using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IMediator mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Create()
    {
        var request = new CreateGameCommand();
        var result = await mediator.Send(request);
        return Ok(result);
    }
}