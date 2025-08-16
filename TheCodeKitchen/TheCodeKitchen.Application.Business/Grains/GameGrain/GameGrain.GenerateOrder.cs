namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    public async Task<Result<TheCodeKitchenUnit>> GenerateOrder()
    {
        var orderNumber = state.State.OrderNumbers.DefaultIfEmpty(0).Max() + 1;

        var game = state.State.Id;
        var orderGrain = GrainFactory.GetGrain<IOrderGrain>(orderNumber, game.ToString());
        
        var generateOrderResult = await orderGrain.Generate();

        if (!generateOrderResult.Succeeded)
            return generateOrderResult.Error;

        state.State.OrderNumbers.Add(orderNumber);
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}