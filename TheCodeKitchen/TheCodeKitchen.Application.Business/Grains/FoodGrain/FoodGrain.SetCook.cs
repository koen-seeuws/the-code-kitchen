using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetCook(SetCookRequest request)
    {
        if (state.State.OrderNumber.HasValue)
            return new AlreadyDeliveredError(
                $"This food is already delivered to order {state.State.OrderNumber.Value}");
        
        if (state.State.Cook != null && state.State.Cook != request.Cook)
            return new AlreadyBeingHeldError(
                $"This food is already being held by another cook");

        state.State.Cook = request.Cook;
        
        state.State.CurrentEquipmentType = null;
        state.State.CurrentEquipmentNumber = null;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}