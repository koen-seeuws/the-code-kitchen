using TheCodeKitchen.Application.Contracts.Requests.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public virtual async Task<Result<TheCodeKitchenUnit>> AddItem(AddItemRequest request)
    {
        if(state.State.AddedItems.Count >= maxItems)
            return new EquipmentFullError($"The equipment {this.GetPrimaryKey()} has reached the maximum number of contained items ({maxItems})");
        
        // TODO: Possibly something
        
        state.State.AddedItems.Add(request.Item);
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}