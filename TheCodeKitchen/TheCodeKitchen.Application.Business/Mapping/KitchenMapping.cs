using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenMapping : Profile
{
    public KitchenMapping()
    {
        //Request - Command
        CreateMap<CreateKitchenRequest, CreateKitchenCommand>();
        
        //Kitchen - Response
        CreateMap<Kitchen, CreateKitchenResponse>();
    }
}