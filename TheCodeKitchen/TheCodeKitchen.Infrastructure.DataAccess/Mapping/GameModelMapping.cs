using AutoMapper;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess.Mapping;

public class GameModelMapping : Profile
{
    public GameModelMapping()
    {
        //Domain - Model
        CreateMap<Game, GameModel>();
        
        //Model - Domain
        CreateMap<GameModel, Game>();
    }
}