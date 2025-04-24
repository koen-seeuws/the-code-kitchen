using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameManagementMapping : Profile
{
    public GameManagementMapping()
    {
        //Request - Command
        CreateMap<CreateGameRequest, CreateGameCommand>();
        CreateMap<JoinGameRequest, JoinGameCommand>();
        
        //Domain - Response
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Game, GetGamesResponse>();
        CreateMap<Game, PauseOrUnpauseGameResponse>();
        
        //Notification - EventDto
        CreateMap<GameCreatedNotification, GameCreatedEventDto>();
        CreateMap<CookJoinedNotification, CookJoinedEventDto>();
        CreateMap<GamePausedOrUnpausedNotification, GamePausedOrUnpausedEventDto>();
    }
}