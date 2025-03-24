namespace TheCodeKitchen.Core.Domain;

public class Cook : IHasGuidId
{
    public Guid Id { get; init; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }

    public Guid KitchenId { get; private set; }
    public Kitchen Kitchen { get; private set; }

    private Cook()
    {
    }

    public Cook(string username, string passwordHash, Kitchen kitchen)
    {
        Id = Guid.CreateVersion7();
        Username = username;
        PasswordHash = passwordHash;
        KitchenId = kitchen.Id;
        Kitchen = kitchen;
    }
}