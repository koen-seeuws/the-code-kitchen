using Orleans;

namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record NotEmptyError : BusinessError
{
    public NotEmptyError()
    {
    }

    public NotEmptyError(string message) : base(message)
    {
    }
}