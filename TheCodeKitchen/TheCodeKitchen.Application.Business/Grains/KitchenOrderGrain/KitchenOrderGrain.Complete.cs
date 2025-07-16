using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Complete()
    {
        if (!state.RecordExists)
        {
            var orderNumber = this.GetPrimaryKeyLong();
            var kitchenId = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
            return new NotFoundError($"The order with number {orderNumber} does not exist in kitchen {kitchenId}");
        }

        var kitchen = GrainFactory.GetGrain<IKitchenGrain>(state.State.Kitchen);
        var closeOrderRequest = new CloseOrderRequest(state.State.Number);
        var closeOrderResult = await kitchen.CloseOrder(closeOrderRequest);

        if (!closeOrderResult.Succeeded)
            return closeOrderResult.Error;

        state.State.Completed = true;
        
        //TODO:
        // Order rating for missing food
        
        
        
        await state.WriteStateAsync();

        DeactivateOnIdle();

        return TheCodeKitchenUnit.Value;
    }
}