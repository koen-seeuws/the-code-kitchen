namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record AlreadyBeingHeldError : BusinessError
{
    public AlreadyBeingHeldError()
    {
    }

    public AlreadyBeingHeldError(string message) : base(message)
    {
    }
}