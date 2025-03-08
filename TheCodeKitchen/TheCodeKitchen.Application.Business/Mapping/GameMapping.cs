using AutoMapper;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Core.Domain;

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