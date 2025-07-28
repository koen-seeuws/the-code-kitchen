using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Contracts.Constants;

public static class TheCodeKitchenEquipmentTypeConstants
{
    public static readonly string[] All = new[]
    {
        EquipmentType.Counter,
        EquipmentType.CuttingBoard,
        EquipmentType.Mixer,
        EquipmentType.Furnace,
        EquipmentType.Bbq,
        EquipmentType.HotPlate
    };
    
    public static readonly string[] Stepable = new[]
    {
        EquipmentType.CuttingBoard,
        EquipmentType.Mixer,
        EquipmentType.Furnace,
        EquipmentType.Bbq
    };
}