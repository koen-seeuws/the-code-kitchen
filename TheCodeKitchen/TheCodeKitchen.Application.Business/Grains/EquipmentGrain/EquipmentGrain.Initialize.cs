using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class EquipmentGrain
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

        var (temperatureTransferRate, temperature) = equipmentType switch
        {
            EquipmentType.Bbq => (0.03, 270),
            EquipmentType.Fridge => (0.005, 3),
            EquipmentType.Freezer => (0.003, -18),
            EquipmentType.Fryer => (0.04, 180),
            EquipmentType.HotPlate => (0.025, 40),
            EquipmentType.Oven => (0.015, 225),
            EquipmentType.Stove => ((double?)0.03, (double?)225),
            _ => (null, null)
        };

        var equipment = new Equipment(request.Game, kitchen, equipmentType, number, temperature,
            temperatureTransferRate);
        state.State = equipment;
        await state.WriteStateAsync();

        return mapper.Map<CreateEquipmentResponse>(equipment);
    }
}