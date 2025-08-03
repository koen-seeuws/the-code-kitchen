namespace TheCodeKitchen.Core.Domain;

public class Cook(string username, string passwordHash, Guid game, Guid kitchen)
{
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;
    public Guid Game { get; set; } = game;
    public Guid Kitchen { get; set; } = kitchen;
    public Guid? Food { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<Timer> Timers { get; set; } = new List<Timer>();
}