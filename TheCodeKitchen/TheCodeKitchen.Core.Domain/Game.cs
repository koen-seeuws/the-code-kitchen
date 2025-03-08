using System.Security.AccessControl;
using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain;

public partial record Game(
    long? Id,
    string Name,
    DateTimeOffset? Started,
    ICollection<Kitchen> Kitchens,
    ICollection<IDomainEvent> Events
);