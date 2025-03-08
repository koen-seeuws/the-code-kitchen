using TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Entities;

public class KitchenModel : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    //Navigation properties
    public long GameId { get; set; }
    public GameModel Game { get; set; }
    
}