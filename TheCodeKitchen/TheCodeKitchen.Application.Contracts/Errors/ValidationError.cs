namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record ValidationError(ICollection<string> ValidationMessages) : Error("Validation error occurred.")
{
    [Id(1)]
    public ICollection<string> ValidationMessages { get; set; } = ValidationMessages;
}