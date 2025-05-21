namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenOrderMapping : Profile
{
    public KitchenOrderMapping()
    {
        CreateMap<KitchenOrder, CreateKitchenOrderResponse>();
    }
}