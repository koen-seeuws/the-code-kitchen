using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;

namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record NotUniqueError : BusinessError
{
    public NotUniqueError()
    {
    }

    public NotUniqueError(string message) : base(message)
    {
    }
}