using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Server.Presentation.API.Models;

namespace TheCodeKitchen.Server.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
    {
        return Ok("token");
    }
}