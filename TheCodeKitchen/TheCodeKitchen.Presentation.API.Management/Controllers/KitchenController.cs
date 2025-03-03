using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Presentation.API.Management.Models.Kitchen;
using TheCodeKitchen.Presentation.API.Management.Requests;

namespace TheCodeKitchen.Presentation.API.Management.Controllers;

[ApiController]
[Route("[controller]")]
public class KitchenController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateKitchenRequestResult>> Create([FromBody] CreateKitchenRequest kitchen)
    {
        var request = mapper.Map<CreateKitchenCommand>(kitchen);
        var result = await mediator.Send(request);
        return this.MatchToAnActionResult(result);
    }
}