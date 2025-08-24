namespace TheCodeKitchen.Core.Domain;

public class Kitchen(Guid id, string name, string code, Guid game, IDictionary<string, int> equipment)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Code { get; set; } = code;
    public double Rating { get; set; } = 1.0;
    public Guid Game { get; set; } = game;
    public ICollection<string> Cooks { get; set; } = new List<string>();
    public IDictionary<string, int> Equipment { get; set; } = equipment;

    public ICollection<long> OpenOrders { get; set; } = new List<long>();
}