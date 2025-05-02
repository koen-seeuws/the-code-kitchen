using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

public class NotFoundException : Error
{
    public NotFoundException()
    {
    }

    public NotFoundException(string? message)
    {
    }
}