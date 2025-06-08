using TheCodeKitchen.Application.Contracts.Response.Pantry;

namespace TheCodeKitchen.Application.Business.Grains.PantryGrain;

public partial class PantryGrain
{
    public async Task<Result<IEnumerable<GetIngredientResponse>>> GetIngredients()
    {
        var ingredients = mapper.Map<List<GetIngredientResponse>>(state.State.Ingredients);
        return ingredients;
    }
}