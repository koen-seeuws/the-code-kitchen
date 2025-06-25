using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Complete()
    {
        var kitchen = GrainFactory.GetGrain<IKitchenGrain>(state.State.Kitchen);
        var closeOrderRequest = new CloseOrderRequest(state.State.Number);
        var closeOrderResult = await kitchen.CloseOrder(closeOrderRequest);
        
        if(!closeOrderResult.Succeeded)
            return closeOrderResult.Error;
        
        state.State.Completed = true;
        await state.WriteStateAsync();

        //TODO: order rating

        return TheCodeKitchenUnit.Value;
    }
}