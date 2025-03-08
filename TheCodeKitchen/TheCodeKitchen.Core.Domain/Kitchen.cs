using System.Security.AccessControl;

namespace TheCodeKitchen.Core.Domain;

public partial record Kitchen(
    long? Id,
    string Name,
    string Code
);