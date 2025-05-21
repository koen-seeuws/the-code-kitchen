using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private async Task<Result<TheCodeKitchenUnit>> GenerateOrder()
    {
        var orderNumber = state.State.OrderNumbers.DefaultIfEmpty(0).Max() + 1;

        var game = this.GetPrimaryKey();
        var orderGrain = GrainFactory.GetGrain<IOrderGrain>(orderNumber, game.ToString());

        //TODO: Implement order generation logic
        logger.LogInformation("Generating order {number}...", orderNumber);
        var orderRequest = new CreateOrderRequest();

        var createOrderResult = await orderGrain.Initialize(orderRequest);

        if (!createOrderResult.Succeeded)
            return createOrderResult.Error;

        state.State.OrderNumbers.Add(orderNumber);
        await state.WriteStateAsync();

        var createKitchenOrderTasks = state.State.Kitchens.Select(kitchen =>
        {
            var kitchenOrderGrain = GrainFactory.GetGrain<IKitchenOrderGrain>(orderNumber, kitchen.ToString());
            var createKitchenOrderRequest = new CreateKitchenOrderRequest();
            return kitchenOrderGrain.Initialize(createKitchenOrderRequest);
        });

        await Task.WhenAll(createKitchenOrderTasks);

        return TheCodeKitchenUnit.Value;
    }
}