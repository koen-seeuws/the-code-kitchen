using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<CreateEquipmentResponse>> Initialize(CreateEquipmentRequest request)
    {
        var kitchen = this.GetPrimaryKey();
        var primaryKeyExtensions = this.GetPrimaryKeyString().Split('+');
        var equipmentType = Enum.Parse<EquipmentType>(primaryKeyExtensions[1]);
        var number = int.Parse(primaryKeyExtensions[2]);

        if (state.RecordExists)
            return new AlreadyExistsError(
                $"The {equipmentType.ToString().ToCamelCase()} equipment in kitchen {kitchen} with number {number}  has already been initialized");

        // State
        var equipment = new Equipment(kitchen, equipmentType, number);
        state.State = equipment;
        await state.WriteStateAsync();

        return mapper.Map<CreateEquipmentResponse>(equipment);
    }
}