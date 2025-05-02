using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

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