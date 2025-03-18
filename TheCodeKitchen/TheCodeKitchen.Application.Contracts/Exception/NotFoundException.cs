namespace TheCodeKitchen.Application.Contracts.Exception;

public class NotFoundException : ApplicationException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}