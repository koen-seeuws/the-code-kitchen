namespace TheCodeKitchen.Cook.Client.Custom;

public record Order(long Number, ICollection<string> RequestedFoods, ICollection<string> DeliveredFoods);