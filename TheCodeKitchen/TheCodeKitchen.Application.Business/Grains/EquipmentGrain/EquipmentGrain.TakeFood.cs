using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request)
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

        if (state.State.Foods.Count <= 0)
            return new EquipmentEmptyError(
                $"The equipment {state.State.EquipmentType} {state.State.Number} does not contain any food");

        if (state.State.Foods.Count > 1)
        {
            var mixResult = await MixFood();
            if (!mixResult.Succeeded)
                return mixResult.Error;
        }

        // TODO: Handling single/merged food item and/or steps from processing in this equipment

        var food = state.State.Foods.First();

        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(food);


        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var holdFoodRequest = new HoldFoodRequest(food);

        var holdFoodResult = await cookGrain.HoldFood(holdFoodRequest);

        if (!holdFoodResult.Succeeded)
            return holdFoodResult.Error;

        if (state.State.Time.HasValue)
        {
            var addStepRequest = new AddStepRequest(state.State.EquipmentType, state.State.Time.Value);
            var addStepResult = await foodGrain.AddStep(addStepRequest);

            if (!addStepResult.Succeeded)
                return addStepResult.Error;

            state.State.Time = null;
        }

        state.State.Foods.Clear();
        await state.WriteStateAsync();

        //TODO: Check if this can be improved (so that this call becomes unnecessary)
        var getFoodResult = await foodGrain.GetFood();
        if (!getFoodResult.Succeeded)
            return getFoodResult.Error;

        return mapper.Map<TakeFoodResponse>(getFoodResult.Value);
    }
}