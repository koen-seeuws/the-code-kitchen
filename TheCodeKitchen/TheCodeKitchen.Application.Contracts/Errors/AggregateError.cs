namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record AggregateError : Error
{
    public AggregateError(IEnumerable<Error> errors)
    {
        Errors = errors;
    }
    
    public AggregateError(string message, IEnumerable<Error> errors) : base(message)
    {
        Errors = errors;
    }

    public IEnumerable<Error> Errors { get; set; }
}