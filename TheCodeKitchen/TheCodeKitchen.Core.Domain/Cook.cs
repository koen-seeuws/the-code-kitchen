namespace TheCodeKitchen.Core.Domain;

public class Cook(Guid id, string username, string passwordHash, Guid kitchen)
{
    public Guid Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;
    public Guid Kitchen { get; set; } = kitchen;
    public Guid? Food { get; set; }
}