namespace TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

public class CreateGameFormModel
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan? TimePerMoment { get; set; } = TimeSpan.FromMinutes(1);
    public double SpeedModifier { get; set; } = 1.0;
    public short MinimumItemsPerOrder { get; set; } = 1;
    public short MaximumItemsPerOrder { get; set; } = 4;
    public double OrderSpeedModifier { get; set; } = 1.0;
    public double Temperature { get; set; } = 30.0;
}