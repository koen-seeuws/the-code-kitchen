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

        // TODO: In relation to TakeFood method TODOs, these 2 may become unnecessary
        CreateMap<GetFoodResponse, TakeFoodResponse>(); 
        CreateMap<FoodDto, SimpleFoodDto>();
    }
}