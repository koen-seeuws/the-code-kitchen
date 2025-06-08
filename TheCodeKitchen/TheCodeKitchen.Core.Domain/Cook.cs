namespace TheCodeKitchen.Core.Domain;

public class Cook(Guid id, string username, string passwordHash, Guid kitchen)
{
    public Guid Id { get; } = id;
    public string Username { get; } = username;
    public string PasswordHash { get; } = passwordHash;
    public Guid Kitchen { get; } = kitchen;
    public Guid? Food { get; set; }
}