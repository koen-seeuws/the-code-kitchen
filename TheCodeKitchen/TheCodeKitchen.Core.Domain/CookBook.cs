namespace TheCodeKitchen.Core.Domain;

public class CookBook(Guid id)
{
    public Guid Id { get; set; } = id;
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}