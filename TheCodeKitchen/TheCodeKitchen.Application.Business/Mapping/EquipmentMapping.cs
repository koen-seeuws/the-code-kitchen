using TheCodeKitchen.Application.Contracts.Response.Equipment;

namespace TheCodeKitchen.Application.Business.Mapping;

public class EquipmentMapping : Profile
{
    public EquipmentMapping()
    {
        CreateMap<Equipment, CreateEquipmentResponse>();
    }
}