using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public sealed partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetOrder(SetOrderRequest request)
    {
        if(!state.RecordExists)
            return new NotFoundError($"The food with id {this.GetPrimaryKey()} does not exist");
        
        if (state.State.OrderNumber.HasValue)
            return new AlreadyDeliveredError(
                $"The food with id {this.GetPrimaryKey()} is already delivered to order {state.State.OrderNumber.Value}");
        
        if (state.State.Cook != null && state.State.Cook != request.Cook)
            return new AlreadyBeingHeldError(
                $"The food with id {this.GetPrimaryKey()} is already being held by another cook");

        state.State.OrderNumber = request.OrderNumber;
        
        state.State.Cook = null;
        state.State.CurrentEquipmentType = null;
        state.State.CurrentEquipmentNumber = null;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}