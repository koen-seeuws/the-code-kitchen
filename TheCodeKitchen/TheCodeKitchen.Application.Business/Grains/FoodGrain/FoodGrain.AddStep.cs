using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> AddStep(AddStepRequest request)
    {
        var step = new RecipeStep(request.EquipmentType, request.Time);
        state.State.Steps.Add(step);
        await state.WriteStateAsync();
        return TheCodeKitchenUnit.Value;
    }
}