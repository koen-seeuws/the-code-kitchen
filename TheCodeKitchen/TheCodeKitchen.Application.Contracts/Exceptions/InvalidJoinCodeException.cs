using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class InvalidJoinCodeException : Error
{
    public InvalidJoinCodeException()
    {
    }

    public InvalidJoinCodeException(string? message)
    {
    }
}