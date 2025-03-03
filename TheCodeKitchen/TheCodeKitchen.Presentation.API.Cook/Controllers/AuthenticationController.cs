using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Presentation.API.Cook.Models;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

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