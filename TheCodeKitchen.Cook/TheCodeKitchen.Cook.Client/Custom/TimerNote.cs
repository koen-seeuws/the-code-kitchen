namespace TheCodeKitchen.Cook.Client.Custom;

public record TimerNote(
    long Order,
    string Food,
    string? IngredientOf,
    string EquipmentType,
    int EquipmentNumber
);