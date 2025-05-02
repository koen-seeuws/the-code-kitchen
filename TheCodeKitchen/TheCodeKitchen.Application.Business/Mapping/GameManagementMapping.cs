using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameManagementMapping : Profile
{
    public GameManagementMapping()
    { 
        //Domain - Response
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Game, GetGameResponse>();
        CreateMap<Game, PauseOrUnpauseGameResponse>();
        
        //Notification - EventDto
        CreateMap<GameCreatedNotification, GameCreatedEventDto>();
        CreateMap<CookJoinedNotification, CookJoinedEventDto>();
        CreateMap<GamePausedOrUnpausedNotification, GamePausedOrUnpausedEventDto>();
    }
}