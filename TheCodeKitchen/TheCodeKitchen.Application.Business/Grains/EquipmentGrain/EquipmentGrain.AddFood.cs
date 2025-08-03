using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<TheCodeKitchenUnit>> AddFood(AddFoodRequest request)
    {
        if (!state.RecordExists)
        {
            var kitchen = this.GetPrimaryKey();
            var primaryKeyExtensions = this.GetPrimaryKeyString().Split('+');
            var equipmentType = primaryKeyExtensions[1];
            var number = int.Parse(primaryKeyExtensions[2]);

            return new AlreadyExistsError(
                $"The equipment {equipmentType} {number} does not exist in kitchen {kitchen}");
        }

        var cookGrain = GrainFactory.GetGrain<ICookGrain>(state.State.Kitchen, request.Cook);
        var releaseFoodResult = await cookGrain.ReleaseFood();

        if (!releaseFoodResult.Succeeded)
            return releaseFoodResult.Error;

        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(releaseFoodResult.Value.Food);
        var setEquipmentRequest =
            new SetEquipmentRequest(state.State.EquipmentType, state.State.Number);

        var setEquipmentResult = await foodGrain.SetEquipment(setEquipmentRequest);

        if (!setEquipmentResult.Succeeded)
            return setEquipmentResult.Error;

        state.State.Time ??= TimeSpan.Zero;

        state.State.Foods.Add(releaseFoodResult.Value.Food);
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}