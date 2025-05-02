using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class NotEmptyException : Error
{
    public NotEmptyException()
    {
    }

    public NotEmptyException(string? message)
    {
    }
}