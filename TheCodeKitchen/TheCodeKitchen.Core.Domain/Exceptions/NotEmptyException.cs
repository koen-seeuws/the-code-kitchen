namespace TheCodeKitchen.Core.Domain.Exceptions;

public class NotEmptyException : DomainException
{
    public NotEmptyException()
    {
    }

    public NotEmptyException(string? message) : base(message)
    {
    }
}