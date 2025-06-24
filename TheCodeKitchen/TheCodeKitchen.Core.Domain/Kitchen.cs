using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string? Code { get; set; } = code;
    public Guid Game { get; } = game;
    public ICollection<Guid> Cooks { get; } = new List<Guid>();
    public IDictionary<EquipmentType, int> Equipment { get; } = new Dictionary<EquipmentType, int>
    {
        { EquipmentType.Blender, 1 },
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 4 },
        { EquipmentType.Furnace, 4 },
        { EquipmentType.HotPlate, 10 },
    };

    public ICollection<long> Orders { get; } = new List<long>();
}