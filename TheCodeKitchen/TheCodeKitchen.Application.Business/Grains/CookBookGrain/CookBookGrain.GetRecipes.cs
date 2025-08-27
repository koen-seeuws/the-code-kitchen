using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Response.CookBook;

namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public sealed partial class CookBookGrain
{
    public Task<Result<IEnumerable<GetRecipeResponse>>> GetRecipes()
    {
        logger.LogInformation("CookBook {CookBook}: Getting all recipes...", state.State.Id);
        var recipes = mapper
            .Map<List<GetRecipeResponse>>(state.State.Recipes)
            .OrderBy(r => r.Name)
            .ToList();
        return Task.FromResult<Result<IEnumerable<GetRecipeResponse>>>(recipes);
    }
}