namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record TimerElapsedEvent(int Number, string Note);