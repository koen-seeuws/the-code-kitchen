namespace TheCodeKitchen.Application.Contracts.Events.GameManagement;

public record GameCreatedEvent(Guid Id, string Name, double SpeedModifier, double Temperature);