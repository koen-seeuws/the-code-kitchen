namespace TheCodeKitchen.Core.Domain;

public class KitchenCook
{
    public long KitchenId { get; set; }
    public Kitchen Kitchen { get; set; }

    public long CookId { get; set; }
    public Cook Cook { get; set; }
}