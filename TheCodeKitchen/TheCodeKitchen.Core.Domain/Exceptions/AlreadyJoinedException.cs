namespace TheCodeKitchen.Core.Domain.Exceptions;

public class AlreadyJoinedException : DomainException
{
    public AlreadyJoinedException()
    {
    }

    public AlreadyJoinedException(string? message) : base(message)
    {
    }
}