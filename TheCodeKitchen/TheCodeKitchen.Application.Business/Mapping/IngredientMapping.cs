using TheCodeKitchen.Application.Contracts.Response.Pantry;

namespace TheCodeKitchen.Application.Business.Mapping;

public class IngredientMapping : Profile
{
    public IngredientMapping()
    {
        CreateMap<Ingredient, CreateIngredientResponse>();
        CreateMap<Ingredient, GetIngredientResponse>();
    }
}