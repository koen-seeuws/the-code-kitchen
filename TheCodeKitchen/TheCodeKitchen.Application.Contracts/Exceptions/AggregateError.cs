using System.Runtime.InteropServices.JavaScript;
using Orleans;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Exceptions;

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