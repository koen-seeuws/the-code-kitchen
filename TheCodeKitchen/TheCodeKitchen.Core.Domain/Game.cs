namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public Guid Id { get; }
    public string Name { get; set; }
    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Paused { get;  set; }
    public ICollection<Guid> Kitchens { get;  set; }
    
    public Game(Guid id, string name)
    {
        Id = id;
        Name = name;
        Kitchens = [];
    }
}