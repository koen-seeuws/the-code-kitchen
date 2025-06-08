using Microsoft.AspNetCore.Mvc;

namespace TheCodeKitchen.Presentation.ManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeBookController(IClusterClient clusterClient) : ControllerBase
{
    
}