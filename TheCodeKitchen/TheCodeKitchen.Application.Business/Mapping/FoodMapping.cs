using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Mapping;

public class FoodMapping : Profile
{
    public FoodMapping()
    {
        CreateMap<Food, CreateFoodResponse>();
    }
}