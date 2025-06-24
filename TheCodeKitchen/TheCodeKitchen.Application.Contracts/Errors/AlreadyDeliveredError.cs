namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record AlreadyDeliveredError : BusinessError
{
    public AlreadyDeliveredError()
    {
    }

    public AlreadyDeliveredError(string message) : base(message)
    {
    }
}