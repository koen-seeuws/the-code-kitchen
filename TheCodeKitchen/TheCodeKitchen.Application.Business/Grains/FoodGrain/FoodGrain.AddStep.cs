using TheCodeKitchen.Application.Contracts.Requests.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public sealed partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> AddStep(AddStepRequest request)
    {
        if(!state.RecordExists)
            return new NotFoundError($"The food with id {this.GetPrimaryKey()} does not exist");
        
        var lastStep = state.State.Steps.LastOrDefault();
        
        if (lastStep is null || lastStep.EquipmentType != request.EquipmentType)
        {
            // If different equipment type in 2 consecutive steps -> Add new step
            var step = new RecipeStep(request.EquipmentType, request.Time);
            state.State.Steps.Add(step);
        }
        else
        {
            // If same equipment type in 2 consecutive steps -> Add up
            lastStep.Time += request.Time;
        }
        
        await state.WriteStateAsync();
        return TheCodeKitchenUnit.Value;
    }
}