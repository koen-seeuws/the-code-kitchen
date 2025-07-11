using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class RecipeStep(EquipmentType equipmentType, TimeSpan time)
{
    public EquipmentType EquipmentType { get; set; } = equipmentType;
    public TimeSpan Time { get; set; } = time;
}