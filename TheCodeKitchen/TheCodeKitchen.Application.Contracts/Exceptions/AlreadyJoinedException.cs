using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class AlreadyJoinedException : Error
{
    public AlreadyJoinedException()
    {
    }

    public AlreadyJoinedException(string? message)
    {
    }
}