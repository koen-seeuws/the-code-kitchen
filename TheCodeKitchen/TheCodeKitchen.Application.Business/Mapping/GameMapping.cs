namespace TheCodeKitchen.Application.Business.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    { 
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Game, GetGameResponse>();
        CreateMap<Game, PauseOrUnpauseGameResponse>();
    }
}