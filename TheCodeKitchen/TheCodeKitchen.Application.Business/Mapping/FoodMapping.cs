using TheCodeKitchen.Application.Contracts.Models.Food;
using TheCodeKitchen.Application.Contracts.Models.Order;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Mapping;

public class FoodMapping : Profile
{
    public FoodMapping()
    {
        CreateMap<Food, CreateFoodResponse>();
        CreateMap<Food, GetFoodResponse>();
        CreateMap<Food, FoodDto>();
        CreateMap<OrderFoodRequest, FoodRequestDto>();

        CreateMap<FoodDto, Food>();
        CreateMap<FoodRequestDto, OrderFoodRequest>();
        CreateMap<FoodRequestDto, KitchenOrderFoodRequest>();

        // TODO: In relation to TakeFood method TODOs, these 2 may become unnecessary
        CreateMap<GetFoodResponse, TakeFoodResponse>(); 
        CreateMap<FoodDto, SimpleFoodDto>();
    }
}