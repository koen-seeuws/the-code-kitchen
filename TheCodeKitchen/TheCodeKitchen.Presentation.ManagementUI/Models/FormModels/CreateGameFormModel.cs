namespace TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

public class CreateGameFormModel
{
    public string Name { get; set; } = string.Empty;
    public double SpeedModifier { get; set; } = 1.0;
    public double Temperature { get; set; } = 30.0;
}