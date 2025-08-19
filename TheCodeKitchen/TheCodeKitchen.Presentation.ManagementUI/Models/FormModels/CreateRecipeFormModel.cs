namespace TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

public class CreateRecipeFormModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<StepFormModel> Steps { get; set; } = new List<StepFormModel>();
    public ICollection<IngredientFormModel> Ingredients { get; set; } = Enumerable.Repeat(new IngredientFormModel(), 2).ToList();
}

public class IngredientFormModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<StepFormModel> Steps { get; set; } = new List<StepFormModel>();
}

public class StepFormModel
{
    public string EquipmentType { get; set; } = string.Empty;
    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
}