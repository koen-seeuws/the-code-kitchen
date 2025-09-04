using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;

namespace TheCodeKitchen.Cook.Client.Custom;

public record TimerNote(
    string Code,
    long Order,
    string Food,
    string[] RecipeTree,
    string? EquipmentType,
    int? EquipmentNumber,
    ICollection<RecipeStepDto> StepsToDo
);