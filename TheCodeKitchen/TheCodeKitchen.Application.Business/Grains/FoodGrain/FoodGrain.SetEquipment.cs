using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetEquipment(SetEquipmentRequest request)
    {
        if (state.State.OrderNumber.HasValue)
            return new AlreadyDeliveredError(
                $"The food with id {this.GetPrimaryKey()} is already delivered to order {state.State.OrderNumber.Value}");
        
        if (state.State.CurrentEquipmentType != null || state.State.CurrentEquipmentNumber != null)
            return new AlreadyBeingHeldError(
                $"The food with id {this.GetPrimaryKey()} is already being held in equipment {state.State.CurrentEquipmentType.ToString()} || {state.State.CurrentEquipmentNumber}");
        
        state.State.CurrentEquipmentType = request.EquipmentType;
        state.State.CurrentEquipmentNumber = request.EquipmentNumber;
        
        state.State.Cook = null;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}