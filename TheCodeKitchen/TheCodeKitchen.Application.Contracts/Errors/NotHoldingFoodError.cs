namespace TheCodeKitchen.Application.Contracts.Errors;

public record NotHoldingFoodError : BusinessError
{
    public NotHoldingFoodError()
    {
    }

    public NotHoldingFoodError(string message) : base(message)
    {
    }
}