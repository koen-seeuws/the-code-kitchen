namespace TheCodeKitchen.Presentation.ManagementUI.Models.TableRecordModels;

public record GameTableRecordModel(
    Guid Id,
    string Name,
    double SpeedModifier,
    double Temperature,
    DateTimeOffset? Started,
    bool Paused
);