namespace TheCodeKitchen.Core.Domain.Exceptions;

public class GameAlreadyStartedException : DomainException
{
    public GameAlreadyStartedException()
    {
    }

    public GameAlreadyStartedException(string? message) : base(message)
    {
    }
}