using TheCodeKitchen.Application.Contracts.Response.Game;

namespace TheCodeKitchen.Application.Business.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        CreateMap<Game, CreateGameResponse>();
        CreateMap<Game, GetGameResponse>()
            .ForCtorParam(
                nameof(GetGameResponse.Paused),
                opt => opt.MapFrom(_ => false)
            );
    }
}