namespace TheCodeKitchen.Application.Contracts.Events.Game;

[GenerateSerializer]
public record NextMomentEvent(double Temperature, TimeSpan? NextMomentDelay);