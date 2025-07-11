using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Mapping;

public class FoodMapping : Profile
{
    public FoodMapping()
    {
        CreateMap<Food, CreateFoodResponse>();
        CreateMap<Food, GetFoodResponse>();
        CreateMap<Food, FoodDto>();

        CreateMap<FoodDto, Food>();

        CreateMap<GetFoodResponse, TakeFoodResponse>(); // TODO: In relation to TakeFood method TODOs, this may become unnecessary
    }
}