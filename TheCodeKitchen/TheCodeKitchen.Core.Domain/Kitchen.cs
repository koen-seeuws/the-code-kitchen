namespace TheCodeKitchen.Core.Domain;

public class Kitchen
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public Guid Game { get; set; }
    public ICollection<Guid> Cooks { get; set; }

    public Kitchen(Guid id, string name, string code, Guid game)
    {
        Id = id;
        Name = name;
        Code = code;
        Game = game;
        Cooks = new List<Guid>();
    }
}