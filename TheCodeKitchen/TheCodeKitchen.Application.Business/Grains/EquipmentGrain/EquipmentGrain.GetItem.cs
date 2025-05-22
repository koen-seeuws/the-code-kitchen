using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public Task<Result<GetItemResponse>> GetItem()
    {
        throw new NotImplementedException();
    }
}