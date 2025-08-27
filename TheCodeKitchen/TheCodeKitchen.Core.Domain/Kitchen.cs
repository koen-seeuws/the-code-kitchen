namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game, Dictionary<string, int> equipment)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Code { get; set; } = code;
    public double Rating { get; set; } = 1.0;
    public Guid Game { get; set; } = game;
    public List<string> Cooks { get; set; } = [];
    public Dictionary<string, int> Equipment { get; set; } = equipment;

    public List<long> OpenOrders { get; set; } = [];
}