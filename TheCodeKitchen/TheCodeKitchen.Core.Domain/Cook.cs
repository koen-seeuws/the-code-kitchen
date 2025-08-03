namespace TheCodeKitchen.Core.Domain;

public class Cook(string username, string passwordHash, Guid kitchen)
{
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;
    public Guid Kitchen { get; set; } = kitchen;
    public Guid? Food { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}