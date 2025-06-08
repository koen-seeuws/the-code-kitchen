using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string? Code { get; set; } = code;
    public Guid Game { get; } = game;
    public ICollection<Guid> Cooks { get; } = new List<Guid>();
    public IDictionary<EquipmentTypes, int> Equipment { get; } = new Dictionary<EquipmentTypes, int>
    {
        { EquipmentTypes.Blender, 1 },
        { EquipmentTypes.Counter, 30 },
        { EquipmentTypes.CuttingBoard, 4 },
        { EquipmentTypes.Furnace, 4 },
        { EquipmentTypes.HotPlate, 10 },
    };

    public ICollection<long> Orders { get; } = new List<long>();
}