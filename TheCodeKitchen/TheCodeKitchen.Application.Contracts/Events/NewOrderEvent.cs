using System.Security.AccessControl;

namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record NewOrderEvent(long Number);