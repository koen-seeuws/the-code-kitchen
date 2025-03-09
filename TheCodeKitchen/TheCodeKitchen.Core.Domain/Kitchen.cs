using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen : DomainObject
{
    public long? Id { get; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    
    public long GameId { get; private set; }
    public Game Game { get; private set; } = null!;

    private Kitchen() { }
    
    public Kitchen(string name, string code, long gameId)
    {
        Name = name;
        Code = code;
        GameId = gameId;
    }
}