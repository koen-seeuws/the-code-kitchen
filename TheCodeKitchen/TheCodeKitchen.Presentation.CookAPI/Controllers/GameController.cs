using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController() : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Join([FromBody] JoinGameRequest request)
    {
        return BadRequest();
    }
}