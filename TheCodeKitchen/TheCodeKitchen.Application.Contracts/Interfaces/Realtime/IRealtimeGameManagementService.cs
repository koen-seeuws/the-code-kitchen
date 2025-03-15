using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealtimeGameManagementService
{
    Task GameCreated(GameCreatedEventDto gameCreatedEvent);
}