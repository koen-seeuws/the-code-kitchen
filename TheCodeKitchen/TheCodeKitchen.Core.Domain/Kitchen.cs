using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Code { get; set; } = code;
    public double Rating { get; set; } = 1.0;
    public Guid Game { get; set; } = game;
    public ICollection<string> Cooks { get; set; } = new List<string>();
    public IDictionary<string, int> Equipment { get; set; } = new Dictionary<string, int>
    {
        { EquipmentType.Bbq, 6 },
        { EquipmentType.Blender, 4 },
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 6 },
        { EquipmentType.Freezer, 12 },
        { EquipmentType.Fridge, 15 },
        { EquipmentType.Fryer, 4 },
        { EquipmentType.HotPlate, 10 },
        { EquipmentType.Mixer, 4 },
        { EquipmentType.Oven, 4 },
        { EquipmentType.Stove, 6 },
    };

    public ICollection<long> OpenOrders { get; set; } = new List<long>();
}