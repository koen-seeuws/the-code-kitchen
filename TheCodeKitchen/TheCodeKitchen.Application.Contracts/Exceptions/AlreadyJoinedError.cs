using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

[GenerateSerializer]
public record AlreadyJoinedError : BusinessError
{
    public AlreadyJoinedError()
    {
    }

    public AlreadyJoinedError(string message) : base(message)
    {
    }
}