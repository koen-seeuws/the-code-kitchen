namespace TheCodeKitchen.Server.Core.Domain;

public class Cook
{
    public string Username { get; set; }
    public string Password { get; set; }

    public Kitchen Kitchen { get; set; }
}