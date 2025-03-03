namespace TheCodeKitchen.Core.Domain;

public class Cook
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    //Navigation properties
    public ICollection<KitchenCook> KitchenCooks { get; set; }

}