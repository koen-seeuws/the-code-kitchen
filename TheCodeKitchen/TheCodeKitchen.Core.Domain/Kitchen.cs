using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string? Code { get; set; } = code;
    public double Rating { get; set; } = 100.0;
    public Guid Game { get; } = game;
    public ICollection<Guid> Cooks { get; } = new List<Guid>();
    public IDictionary<string, int> Equipment { get; } = new Dictionary<string, int>
    {
        {EquipmentType.Bbq, 6},
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 6 },
        { EquipmentType.Furnace, 6 },
        { EquipmentType.HotPlate, 10 },
        { EquipmentType.Mixer, 4 },
    };

    public ICollection<long> OpenOrders { get; } = new List<long>();
}