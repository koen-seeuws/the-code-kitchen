using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        CreateMap<JoinGameRequest, JoinGameCommand>();
    }
}