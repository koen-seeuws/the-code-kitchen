namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Complete()
    {
        if (state.State.Completed)
            return new OrderAlreadyCompletedError($"The order with number {state.State.Number} has already been completed, you cannot complete it again");
        
        state.State.Completed = true;
        await state.WriteStateAsync();

        //TODO: order rating

        return TheCodeKitchenUnit.Value;
    }
}