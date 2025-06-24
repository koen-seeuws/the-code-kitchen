using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Equipment(Guid kitchen, EquipmentType equipmentType, int number)
{
    public Guid Kitchen { get; } = kitchen;
    public EquipmentType EquipmentType { get; set; } = equipmentType;

    public int Number { get; } = number;
    public ICollection<Guid> Foods { get; set; } = new List<Guid>();
}