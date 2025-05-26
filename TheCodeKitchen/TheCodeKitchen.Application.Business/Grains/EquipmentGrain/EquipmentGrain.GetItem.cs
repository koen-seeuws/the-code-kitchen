using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public virtual async Task<Result<GetItemResponse>> GetItem()
    {
        if(state.State.AddedItems.Count <= 0)
            return new EquipmentEmptyError($"The equipment {this.GetPrimaryKey()} does not contain any items that are ready");

        throw new NotImplementedException();
    }
}