namespace TheCodeKitchen.Core.Domain.Exceptions;

public class NotUniqueException : DomainException
{
    public NotUniqueException()
    {
    }

    public NotUniqueException(string? message) : base(message)
    {
    }
}