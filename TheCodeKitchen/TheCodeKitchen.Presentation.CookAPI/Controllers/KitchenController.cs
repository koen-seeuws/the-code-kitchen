using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains.Equipment;
using TheCodeKitchen.Core.Enums;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class KitchenController(IClusterClient clusterClient) : ControllerBase
{
    [HttpPost("[action]/{number:int}")]
    public IActionResult Blender(int number)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var grain = clusterClient.GetGrain<IBlenderGrain>(kitchen, number.ToString());
        
        
        return Ok();
    }
    
    [HttpPost("[action]/{number:int}")]
    public IActionResult Furnace(int number)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var grain = clusterClient.GetGrain<IFurnaceGrain>(kitchen, number.ToString());
        
        
        return Ok();
    }
  
}