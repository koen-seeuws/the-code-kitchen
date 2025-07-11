namespace TheCodeKitchen.Core.Domain;

public class RecipeStep(string equipmentType, TimeSpan time)
{
    public string EquipmentType { get; set; } = equipmentType;
    public TimeSpan Time { get; set; } = time;
}