namespace TheCodeKitchen.Core.Domain.Exceptions;

public class InvalidJoinCodeException : DomainException
{
    public InvalidJoinCodeException()
    {
    }

    public InvalidJoinCodeException(string? message) : base(message)
    {
    }
}