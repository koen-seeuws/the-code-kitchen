using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenManagementMapping : Profile
{
    public KitchenManagementMapping()
    {
        //Request - Command
        CreateMap<AddKitchenRequest, AddKitchenCommand>();
        
        //Domain - Response
        CreateMap<Kitchen, AddKitchenResponse>();
        CreateMap<Kitchen, GetKitchensResponse>();
        
        //Notification - EventDto
        CreateMap<KitchenAddedNotification, KitchenAddedEventDto>();
    }
}