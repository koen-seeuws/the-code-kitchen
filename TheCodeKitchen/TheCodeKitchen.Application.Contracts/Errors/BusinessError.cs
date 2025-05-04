using Orleans;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Errors;

[GenerateSerializer]
public record BusinessError : Error
{
    public BusinessError()
    {
        
    }
    
    public BusinessError(string message) : base(message)
    {
    }
}