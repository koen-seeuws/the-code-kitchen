namespace TheCodeKitchen.Presentation.ManagementUI.Models.TableRecordModels;

public record GameTableRecordModel(
    Guid Id,
    string Name,
    TimeSpan TimePerMoment,
    double SpeedModifier,
    double Temperature,
    DateTimeOffset? Started,
    TimeSpan TimePassed,
    bool Paused
);