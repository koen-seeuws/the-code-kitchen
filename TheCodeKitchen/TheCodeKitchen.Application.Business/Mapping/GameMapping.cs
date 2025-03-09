using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        //Request - Command
        CreateMap<CreateGameRequest, CreateGameCommand>();
        
        //Game - Response
        CreateMap<Game, CreateGameResponse>();
    }
}