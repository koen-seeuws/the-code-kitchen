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
    
    [HttpPost("/{number:int}")]
    public IActionResult Furnace(int number)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var grain = clusterClient.GetGrain<IEquipmentGrain>(kitchen, number.ToString(), EquipmentTypes.Furnace);
        
        
        return Ok();
    }
    
    [HttpPost("[action]/{number:int}")]
    public IActionResult HotPlate(int number)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var grain = clusterClient.GetGrain<IEquipmentGrain>(kitchen, number.ToString(), EquipmentTypes.Furnace);
        
        
        return Ok();
    }
    
    [HttpPost("[action]/{number:int}")]
    public IActionResult Oven(int number)
    {
        
        
        
        return Ok();
    }
}