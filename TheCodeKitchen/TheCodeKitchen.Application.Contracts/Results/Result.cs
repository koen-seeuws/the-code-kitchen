namespace TheCodeKitchen.Application.Contracts.Results;

public class Result<T>
{
    public bool Succeeded { get; init; }
    public T Data { get; set; }
    public Error Error { get; set; }

    public static implicit operator Result<T>(T data)
        => new()
        {
            Succeeded = true,
            Data = data
        };

    public static implicit operator Result<T>(Error error)
        => new()
        {
            Succeeded = false,
            Error = error
        };

    public TMatch Match<TMatch>(Func<T, TMatch> onSuccess, Func<Error, TMatch> onFail)
        => Succeeded ? onSuccess(Data) : onFail(Error);
    
    public static Result<IEnumerable<T>> Combine(IEnumerable<Result<T>> results)
    {
        var values = new List<T>();
        var errors = new List<Error>();

        foreach (var result in results)
        {
            if (result.Succeeded)
            {
                values.Add(result.Data);
            }
            else
            {
                errors.Add(result.Error);
            }
        }

        if (errors.Any())
        {
            return new Error();
        }

        return values;
    }
}