namespace TheCodeKitchen.Application.Business.Mapping;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Order, CreateOrderResponse>();
    }
}