namespace TheCodeKitchen.Core.Domain.Abstractions;

public class DomainException : ApplicationException
{
    protected DomainException()
    {
    }

    protected DomainException(string? message) : base(message)
    {
    }
}