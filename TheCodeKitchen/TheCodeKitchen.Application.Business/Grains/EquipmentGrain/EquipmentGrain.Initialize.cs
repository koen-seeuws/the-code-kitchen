using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    public async Task<Result<CreateEquipmentResponse>> Initialize(CreateEquipmentRequest request)
    {
        var kitchen = this.GetPrimaryKey();
        var number = int.Parse(this.GetPrimaryKeyString());
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The {GetType().ToString().Replace("Grain", string.Empty)} equipment in kitchen {kitchen} with number {number}  has already been initialized");
        
        // State
        var equipment = new Equipment(request.Game, kitchen, number);
        state.State = equipment;
        await state.WriteStateAsync();
        
        // Streams
        await SubscribeToNextMomentEvent();
        
        return mapper.Map<CreateEquipmentResponse>(kitchen);


    }
}