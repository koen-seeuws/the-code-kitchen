namespace TheCodeKitchen.Core.Domain;

public class Cook : DomainEntity, IHasGuidId
{
    public Guid Id { get; init; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Guid Kitchen { get; set; }
    

    public Cook(Guid id, string username, string passwordHash, Guid kitchen)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Kitchen = kitchen;
    }
}