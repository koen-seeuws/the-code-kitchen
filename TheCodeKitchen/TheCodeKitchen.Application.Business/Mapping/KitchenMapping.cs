using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenMapping : Profile
{
    public KitchenMapping()
    {
        //Request - Command
        CreateMap<CreateKitchenRequest, CreateKitchenCommand>();
        
        //Domain - Response
        CreateMap<Kitchen, CreateKitchenResponse>();
        
        //Notification - EventDto
        CreateMap<KitchenCreatedNotification, KitchenCreatedEventDto>();
    }
}