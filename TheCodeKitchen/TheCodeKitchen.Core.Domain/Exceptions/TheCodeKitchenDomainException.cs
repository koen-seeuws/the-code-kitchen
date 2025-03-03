namespace TheCodeKitchen.Core.Domain.Exceptions;

public class TheCodeKitchenDomainException : ApplicationException
{
    public TheCodeKitchenDomainException()
    {
    }

    public TheCodeKitchenDomainException(string? message) : base(message)
    {
    }
}