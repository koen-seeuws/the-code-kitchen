using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenManagementMapping : Profile
{
    public KitchenManagementMapping()
    {
//Domain - Response
        CreateMap<Kitchen, CreateKitchenResponse>();
        CreateMap<Kitchen, GetKitchenResponse>();

        //Notification - EventDto
        CreateMap<KitchenAddedNotification, KitchenAddedEventDto>();
    }
}