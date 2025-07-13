using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> SetEquipment(SetEquipmentRequest request)
    {
        if(!state.RecordExists)
            return new NotFoundError($"The food with id {this.GetPrimaryKey()} does not exist");
        
        if (state.State.OrderNumber.HasValue)
            return new AlreadyDeliveredError(
                $"This food is already delivered to order {state.State.OrderNumber.Value}");
        
        if (!string.IsNullOrWhiteSpace(state.State.CurrentEquipmentType) || state.State.CurrentEquipmentNumber != null)
            return new AlreadyBeingHeldError(
                $"This food is already being held in equipment {state.State.CurrentEquipmentType} || {state.State.CurrentEquipmentNumber}");
        
        state.State.CurrentEquipmentType = request.EquipmentType;
        state.State.CurrentEquipmentNumber = request.EquipmentNumber;
        
        state.State.Cook = null;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}