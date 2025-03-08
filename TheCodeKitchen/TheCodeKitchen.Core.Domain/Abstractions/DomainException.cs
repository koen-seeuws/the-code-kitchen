namespace TheCodeKitchen.Core.Domain.Abstractions;

public class DomainException : ApplicationException
{
    public DomainException()
    {
    }

    public DomainException(string? message) : base(message)
    {
    }
}