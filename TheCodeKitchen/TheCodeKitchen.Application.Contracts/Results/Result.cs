using Orleans;
using TheCodeKitchen.Application.Contracts.Errors;

namespace TheCodeKitchen.Application.Contracts.Results;

[GenerateSerializer]
public class Result<T>
{
    [Id(0)] public bool Succeeded { get; init; }
    [Id(1)] public T Value { get; set; }
    [Id(2)] public Error Error { get; set; }

    public static implicit operator Result<T>(T data)
        => new()
        {
            Succeeded = true,
            Value = data
        };

    public static implicit operator Result<T>(Error error)
        => new()
        {
            Succeeded = false,
            Error = error
        };

    public TMatch Match<TMatch>(Func<T, TMatch> onSuccess, Func<Error, TMatch> onFail)
        => Succeeded ? onSuccess(Value) : onFail(Error);

    public static Result<IEnumerable<T>> Combine(IEnumerable<Result<T>> results)
    {
        var values = new List<T>();
        var errors = new List<Error>();

        foreach (var result in results)
        {
            if (result.Succeeded)
                values.Add(result.Value);
            else
                errors.Add(result.Error);
        }

        return errors.Count == 0 ? values : new AggregateError(errors);
    }
}