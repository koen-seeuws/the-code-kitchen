namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Table
{
    public long Id { get; set; }
    public short Spots { get; set; }
    public short Number { get; set; }

    public long GameId { get; set; }
    public Game Game { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}