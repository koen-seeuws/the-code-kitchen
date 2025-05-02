using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class NotUniqueException : Error
{
    public NotUniqueException()
    {
    }

    public NotUniqueException(string? message)
    {
    }
}