namespace TheCodeKitchen.Presentation.ManagementUI.Models.ViewModels;

public class MessageViewModel
{
    public string From { get; set; }
    public string? To { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string Content { get; set; }
}