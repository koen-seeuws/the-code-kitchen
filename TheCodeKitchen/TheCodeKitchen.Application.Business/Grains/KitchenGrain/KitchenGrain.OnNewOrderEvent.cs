using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task OnNewOrderEvent(NewOrderEvent newOrderEvent, StreamSequenceToken _)
    {
        logger.LogInformation("Kitchen {KitchenId}: received new order {OrderNumber}", state.State.Id, newOrderEvent.Number);
        
        var createKitchenOrderRequest = new CreateKitchenOrderRequest(state.State.Id, newOrderEvent.Number);

        var kitchenOrderGrain = GrainFactory.GetGrain<IKitchenOrderGrain>(newOrderEvent.Number, state.State.Id.ToString());
        var createKitchenOrderResult = await kitchenOrderGrain.Initialize(createKitchenOrderRequest);

        if (createKitchenOrderResult.Succeeded)
        {
            state.State.Orders.Add(newOrderEvent.Number);
            await state.WriteStateAsync();
        }
    }
}