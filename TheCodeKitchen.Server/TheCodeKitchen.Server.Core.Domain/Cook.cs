namespace TheCodeKitchen.Server.Core.Domain;

public class Cook
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    //Navigation properties
    public int KitchenId { get; set; }
    public Kitchen Kitchen { get; set; }
}