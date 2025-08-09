using AutoMapper;
using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;
using TheCodeKitchen.Presentation.ManagementUI.Models.TableRecordModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Mapping;

public class KitchenMapping : Profile
{
    public KitchenMapping()
    {
        CreateMap<GetKitchenResponse, KitchenTableRecordModel>();
        CreateMap<CreateKitchenResponse, KitchenTableRecordModel>();
        CreateMap<KitchenCreatedEvent, KitchenTableRecordModel>();
    }
}