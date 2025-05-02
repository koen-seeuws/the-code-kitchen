using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

[GenerateSerializer]
public record GameAlreadyStartedError : BusinessError
{
    public GameAlreadyStartedError()
    {
    }

    public GameAlreadyStartedError(string message) : base(message)
    {
    }
}