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
        CreateMap<JoinGameRequest, JoinGameCommand>();
        
        //Domain - Response
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Game, GetGamesResponse>();
        
        //Notification - EventDto
        CreateMap<GameCreatedNotification, GameCreatedEventDto>();
        CreateMap<CookJoinedNotification, CookJoinedEventDto>();

    }
}