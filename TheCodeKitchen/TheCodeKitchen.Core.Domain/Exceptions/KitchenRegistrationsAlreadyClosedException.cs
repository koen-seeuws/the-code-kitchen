namespace TheCodeKitchen.Core.Domain.Exceptions;

public class KitchenRegistrationsAlreadyClosedException : DomainException
{
    public KitchenRegistrationsAlreadyClosedException()
    {
    }

    public KitchenRegistrationsAlreadyClosedException(string? message) : base(message)
    {
    }
}