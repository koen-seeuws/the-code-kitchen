using Orleans;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

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