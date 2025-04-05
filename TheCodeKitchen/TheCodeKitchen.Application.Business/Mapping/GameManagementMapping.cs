using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameManagementMapping : Profile
{
    public GameManagementMapping()
    {
        //Request - Command
        CreateMap<CreateGameRequest, CreateGameCommand>();
        CreateMap<AddKitchenRequest, AddKitchenCommand>();
        CreateMap<JoinGameRequest, JoinGameCommand>();
        
        //Domain - Response
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Kitchen, AddKitchenResponse>();
        
        //Notification - EventDto
        CreateMap<GameCreatedNotification, GameCreatedEventDto>();
        CreateMap<KitchenAddedNotification, KitchenAddedEventDto>();
        CreateMap<CookJoinedNotification, CookJoinedEventDto>();

    }
}