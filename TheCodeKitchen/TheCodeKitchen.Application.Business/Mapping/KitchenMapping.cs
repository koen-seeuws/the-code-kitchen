using AutoMapper;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Response;

namespace TheCodeKitchen.Application.Business.Mapping;

public class KitchenMapping : Profile
{
    public KitchenMapping()
    {
        //Request - Command
        CreateMap<CreateKitchenRequest, CreateKitchenCommand>();
        
        //Kitchen - Response
        CreateMap<Kitchen, CreateKitchenResponse>();
    }
}