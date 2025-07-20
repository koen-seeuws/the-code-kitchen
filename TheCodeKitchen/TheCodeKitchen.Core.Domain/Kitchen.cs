using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Code { get; set; } = code;
    public double Rating { get; set; } = 100.0;
    public Guid Game { get; set; } = game;
    public ICollection<Guid> Cooks { get; set; } = new List<Guid>();
    public IDictionary<string, int> Equipment { get; set; } = new Dictionary<string, int>
    {
        {EquipmentType.Bbq, 6},
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 6 },
        { EquipmentType.Furnace, 6 },
        { EquipmentType.HotPlate, 10 },
        { EquipmentType.Mixer, 4 },
    };

    public ICollection<long> OpenOrders { get; set; } = new List<long>();
}