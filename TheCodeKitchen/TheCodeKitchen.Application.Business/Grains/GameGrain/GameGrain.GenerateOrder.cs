using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    private async Task<Result<TheCodeKitchenUnit>> GenerateOrder()
    {
        var orderNumber = state.State.OrderNumbers.DefaultIfEmpty(0).Max() + 1;

        var game = state.State.Id;
        var orderGrain = GrainFactory.GetGrain<IOrderGrain>(orderNumber, game.ToString());

        //TODO: Implement order generation logic
        logger.LogInformation("Game {gameId}: Generating order {number}...", state.State.Id, orderNumber);
        
        var orderRequest = new CreateOrderRequest();

        var createOrderResult = await orderGrain.Initialize(orderRequest);

        if (!createOrderResult.Succeeded)
            return createOrderResult.Error;

        state.State.OrderNumbers.Add(orderNumber);
        await state.WriteStateAsync();
        
        var newOrderEvent = new NewOrderEvent(orderNumber);
        
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.AzureStorageQueuesProvider);
        var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Id);
        await stream.OnNextAsync(newOrderEvent);
        
        await PickSecondsUntilNextOrder();

        return TheCodeKitchenUnit.Value;
    }
}