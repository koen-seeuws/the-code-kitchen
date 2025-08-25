namespace TheCodeKitchen.Application.Contracts.Results;

[GenerateSerializer]
public abstract record Error
{   
    [Id(0)] public string Message { get; set; }
    
    public Error()
    {
        Message = string.Empty;
    }

    public Error(string message)
    {
        Message = message;
    }
}