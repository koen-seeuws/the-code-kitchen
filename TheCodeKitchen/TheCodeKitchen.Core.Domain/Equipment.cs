namespace TheCodeKitchen.Core.Domain;

public class Equipment(Guid kitchen, string equipmentType, int number)
{
    public Guid Kitchen { get; } = kitchen;
    public string EquipmentType { get; set; } = equipmentType;

    public int Number { get; } = number;
    public TimeSpan? Time { get; set; }
    public ICollection<Guid> Foods { get; set; } = new List<Guid>();
}