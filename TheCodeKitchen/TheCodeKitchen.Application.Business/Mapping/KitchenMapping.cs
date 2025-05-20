namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenMapping : Profile
{
    public KitchenMapping()
    {
        CreateMap<Kitchen, CreateKitchenResponse>();
        CreateMap<Kitchen, GetKitchenResponse>();
    }
}