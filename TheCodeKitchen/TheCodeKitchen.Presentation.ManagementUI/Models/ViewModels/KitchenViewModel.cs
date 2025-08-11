namespace TheCodeKitchen.Presentation.ManagementUI.Models.ViewModels;

public class KitchenViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public double Rating { get; set; }
}