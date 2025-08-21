using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers;

[Tags("Communication")]
[ApiController]
[Route("[controller]")]
[Authorize]
public class CommunicationController(IClusterClient clusterClient) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> SendMessage(SendMessageRequest request)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var cook = HttpContext.User.GetUsername();
        var kitchenGrain = clusterClient.GetGrain<IKitchenGrain>(kitchen);
        var deliverMessageToKitchenRequest = new DeliverMessageToKitchenRequest(cook, request.To, request.Content);
        var result = await kitchenGrain.DeliverMessage(deliverMessageToKitchenRequest);
        return this.MatchActionResult(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ReadMessages()
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var cook = HttpContext.User.GetUsername();
        var cookGrain = clusterClient.GetGrain<ICookGrain>(kitchen, cook);
        var result = await cookGrain.ReadMessages();
        return this.MatchActionResult(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Confirm(ConfirmMessageRequest request)
    {
        var kitchen = HttpContext.User.GetKitchenId();
        var cook = HttpContext.User.GetUsername();
        var cookGrain = clusterClient.GetGrain<ICookGrain>(kitchen, cook);
        var result = await cookGrain.ConfirmMessage(request);
        return this.MatchActionResult(result);
    }
}