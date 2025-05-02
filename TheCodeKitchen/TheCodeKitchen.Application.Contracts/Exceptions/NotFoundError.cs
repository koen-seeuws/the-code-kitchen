using Orleans;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

[GenerateSerializer]
public record NotFoundError : Error
{
    public NotFoundError()
    {
    }

    public NotFoundError(string message) : base(message)
    {
    }
}