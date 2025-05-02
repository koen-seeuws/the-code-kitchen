using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

[GenerateSerializer]
public record InvalidJoinCodeError : BusinessError
{
    public InvalidJoinCodeError()
    {
    }

    public InvalidJoinCodeError(string message) : base(message)
    {
    }
}