using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private async Task GenerateOrder()
    {
        var orderRequest = new CreateOrderRequest();
        
        var orderNumber = state.State.OrderNumbers.DefaultIfEmpty(0).Max() + 1;
        
        //TODO: Implement order generation logic
        logger.LogInformation("Generating order {number}...", orderNumber);
        
        var orderGrain = GrainFactory.GetGrain<IOrderGrain>(orderNumber, this.GetPrimaryKey().ToString());
        var result = await orderGrain.Initialize(orderRequest);
        
        

        
        

        state.State.OrderNumbers.Add(orderNumber);
        await state.WriteStateAsync();
    }
}