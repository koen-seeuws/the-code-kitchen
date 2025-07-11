using TheCodeKitchen.Application.Contracts.Response.CookBook;

namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public partial class CookBookGrain
{
    public Task<Result<IEnumerable<GetRecipeResponse>>> GetRecipes()
    {
        var recipes = mapper.Map<List<GetRecipeResponse>>(state.State.Recipes);
        return Task.FromResult<Result<IEnumerable<GetRecipeResponse>>>(recipes);
    }
}