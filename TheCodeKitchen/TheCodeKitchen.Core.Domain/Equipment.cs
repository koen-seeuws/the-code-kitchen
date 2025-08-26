namespace TheCodeKitchen.Core.Domain;

public class Equipment(Guid game, Guid kitchen, string equipmentType, int number)
{
    public Guid Game { get; set; } = game;
    public Guid Kitchen { get; set; } = kitchen;
    public string EquipmentType { get; set; } = equipmentType;

    public int Number { get; set; } = number;
    public TimeSpan? Time { get; set; }
    public List<Guid> Foods { get; set; } = new();
}