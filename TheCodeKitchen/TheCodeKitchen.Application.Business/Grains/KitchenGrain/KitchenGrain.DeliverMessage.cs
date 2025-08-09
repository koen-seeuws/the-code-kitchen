using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeliverMessage(DeliverMessageToKitchenRequest request)
    {
        var cooks = state.State.Cooks;

        if (!string.IsNullOrWhiteSpace(request.To))
        {
            var cook = cooks.FirstOrDefault(c => c.Equals(request.To, StringComparison.OrdinalIgnoreCase));
            if (cook is null)
                return new NotFoundError($"The cook with username {request.To} does not exist in your kitchen");
            cooks = [cook];
        }

        var timestamp = DateTime.UtcNow;

        var tasks = cooks
            .Where(cook => !cook.Equals(request.From, StringComparison.OrdinalIgnoreCase))
            .Select(cook =>
            {
                var cookGrain = GrainFactory.GetGrain<ICookGrain>(state.State.Id, cook);
                var deliverMessageRequest =
                    new DeliverMessageToCookRequest(request.From, cook, request.Content, timestamp);
                return cookGrain.DeliverMessage(deliverMessageRequest);
            });

        await Task.WhenAll(tasks);
        
        var @event = new MessageDeliveredEvent(request.From, request.To, request.Content, timestamp);
        await realTimeKitchenService.SendMessageDeliveredEvent(state.State.Id, @event);

        return TheCodeKitchenUnit.Value;
    }
}