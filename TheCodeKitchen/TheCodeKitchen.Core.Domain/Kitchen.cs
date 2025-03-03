namespace TheCodeKitchen.Core.Domain;

public class Kitchen
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    //Navigation properties
    public long GameId { get; set; }
    public Game Game { get; set; }
    
    public ICollection<KitchenCook> KitchenCooks { get; set; }
    public ICollection<KitchenOrder> KitchenOrders { get; set; }
}