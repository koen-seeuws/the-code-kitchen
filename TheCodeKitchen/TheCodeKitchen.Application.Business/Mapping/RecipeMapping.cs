using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Response.CookBook;

namespace TheCodeKitchen.Application.Business.Mapping;

public class RecipeMapping : Profile
{
    public RecipeMapping()
    {
        CreateMap<Recipe, GetRecipeResponse>();
        CreateMap<RecipeIngredient, RecipeIngredientDto>();
        CreateMap<RecipeStep, RecipeStepDto>();
        
        CreateMap<RecipeStepDto, RecipeStep>();
    }
}