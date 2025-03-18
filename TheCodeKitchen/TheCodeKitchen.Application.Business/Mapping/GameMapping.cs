using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        //Request - Command
        CreateMap<CreateGameRequest, CreateGameCommand>();
        
        //Domain - Response
        CreateMap<Game, CreateGameResponse>();
        
        //Notification - EventDto
        CreateMap<GameCreatedNotification, GameCreatedEventDto>();
    }
}