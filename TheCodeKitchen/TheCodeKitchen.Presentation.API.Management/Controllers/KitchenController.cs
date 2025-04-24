using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Queries;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class KitchenController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddKitchenRequest addKitchenRequest, CancellationToken cancellationToken = default)
    {
        var request = mapper.Map<AddKitchenCommand>(addKitchenRequest);
        var result = await mediator.Send(request, cancellationToken);
        return this.MatchActionResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetKitchens([FromQuery] Guid gameId, CancellationToken cancellationToken = default)
    {
        var query = new GetKitchensQuery(gameId);
        var result = await mediator.Send(query, cancellationToken);
        return this.MatchActionResult(result);
    }
}