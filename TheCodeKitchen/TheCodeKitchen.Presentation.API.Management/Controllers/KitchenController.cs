using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Presentation.API.Management.Models.Kitchen;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class KitchenController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKitchenRequest createKitchenRequest, CancellationToken cancellationToken = default)
    {
        var request = mapper.Map<CreateKitchenCommand>(createKitchenRequest);
        var result = await mediator.Send(request, cancellationToken = default);
        return this.MatchActionResult(result);
    }
}