using AutoMapper;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess.Mapping;

public class KitchenModelMapping : Profile
{
    public KitchenModelMapping()
    {
        //Domain - Model
        CreateMap<Kitchen, KitchenModel>();
        
        //Model - Domain
        CreateMap<KitchenModel, Kitchen>();
    }
}