using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<CreateEquipmentResponse>> Initialize(CreateEquipmentRequest request)
    {
        var kitchen = this.GetPrimaryKey();
        var primaryKeyExtensions = this.GetPrimaryKeyString().Split('+');
        var equipmentType = primaryKeyExtensions[1];
        var number = int.Parse(primaryKeyExtensions[2]);

        if (state.RecordExists)
            return new AlreadyExistsError(
                $"The equipment {equipmentType} {number} has already been initialized in kitchen {kitchen}");
        
        var equipment = new Equipment(kitchen, equipmentType, number);
        state.State = equipment;
        await state.WriteStateAsync();

        return mapper.Map<CreateEquipmentResponse>(equipment);
    }
}