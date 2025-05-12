using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record NotImplementedError : Error
{
    public NotImplementedError()
    {
    }

    public NotImplementedError(string message) : base(message)
    {
    }
}