namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record EquipmentFullError : BusinessError
{
    public EquipmentFullError()
    {
    }

    public EquipmentFullError(string message) : base(message)
    {
    }
}