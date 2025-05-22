namespace TheCodeKitchen.Core.Domain;

public class Equipment
{
    public Guid Game { get; set; }
    public Guid Kitchen { get; set; }
    public int Number { get; set; }
    public bool On { get; set; }
    public ICollection<Guid> Items { get; set; }
    
    //public Guid CurrentItem { get; set; }

    public Equipment(Guid game, Guid kitchen, int number)
    {
        Game = game;
        Kitchen = kitchen;
        Number = number;
        Items = new List<Guid>();
    }
}