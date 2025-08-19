using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Contracts.Constants;

public static class TheCodeKitchenEquipmentTypeConstants
{
    public static readonly string[] All = new[]
    {        
        EquipmentType.Bbq,
        EquipmentType.Blender,
        EquipmentType.Counter,
        EquipmentType.CuttingBoard,
        EquipmentType.Fridge,
        EquipmentType.Freezer,
        EquipmentType.Fryer,
        EquipmentType.HotPlate,
        EquipmentType.Mixer,
        EquipmentType.Oven,
        EquipmentType.Stove,
    };
    
    public static readonly string[] Steppable = new[]
    {       
        EquipmentType.Bbq,
        EquipmentType.Blender,
        EquipmentType.CuttingBoard,
        EquipmentType.Fridge,
        EquipmentType.Freezer,
        EquipmentType.Fryer,
        EquipmentType.Mixer,
        EquipmentType.Oven,
        EquipmentType.Stove
    };
}