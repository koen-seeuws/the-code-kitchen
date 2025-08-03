using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public sealed partial class FoodGrain
{
    public async Task<Result<CreateFoodResponse>> Initialize(CreateFoodRequest request)
    {
        if (state.RecordExists)
            return new AlreadyExistsError($"The food grain with id {this.GetPrimaryKey()} has already been initialized");

        var id = this.GetPrimaryKey();
        var name = request.Name.Trim().ToCamelCase();
        var foods = mapper.Map<List<Food>>(request.Foods);
        
        var food = new Food(id, name, request.Temperature, request.Game, request.Kitchen, foods);

        state.State = food;
        await state.WriteStateAsync();

        await SubscribeToNextMomentEvent();

        return mapper.Map<CreateFoodResponse>(food);
    }
}