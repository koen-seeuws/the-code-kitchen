namespace TheCodeKitchen.Presentation.API.Cook.Models;

public class AuthenticateRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public long KitchenId { get; set; }
}