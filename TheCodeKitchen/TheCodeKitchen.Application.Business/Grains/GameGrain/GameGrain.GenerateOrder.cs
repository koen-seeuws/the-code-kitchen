using TheCodeKitchen.Application.Contracts.Requests.Order;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    public async Task<Result<TheCodeKitchenUnit>> GenerateOrder()
    {
        var orderNumber = state.State.OrderNumbers.DefaultIfEmpty(0).Max() + 1;

        var game = state.State.Id;
        var orderGrain = GrainFactory.GetGrain<IOrderGrain>(orderNumber, game.ToString());

        var generateOrderRequest =
            new GenerateOrderRequest(state.State.MinimumItemsPerOrder, state.State.MaximumItemsPerOrder);
        var generateOrderResult = await orderGrain.GenerateOrder(generateOrderRequest);

        if (!generateOrderResult.Succeeded)
            return generateOrderResult.Error;

        state.State.OrderNumbers.Add(orderNumber);
        await state.WriteStateAsync();

        _timeUntilNewOrder = generateOrderResult.Value.MinimumTimeToPrepare / state.State.OrderSpeedModifier;

        return TheCodeKitchenUnit.Value;
    }
}