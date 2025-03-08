using TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Entities;

public class GameModel : BaseEntity
{
    public long Id { get; set; }

    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Paused { get; set; }

    //Navigation properties
    public ICollection<KitchenModel> Kitchens { get; set; }
}