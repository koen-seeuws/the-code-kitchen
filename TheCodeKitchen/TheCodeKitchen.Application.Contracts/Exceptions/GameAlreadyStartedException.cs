using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class GameAlreadyStartedException : Error
{
    public GameAlreadyStartedException()
    {
    }

    public GameAlreadyStartedException(string? message)
    {
    }
}