namespace TheCodeKitchen.Application.Contracts.Results;

public class Result<T>
{
    public bool Succeeded { get; init; }
    public T Data { get; set; }
    public System.Exception Error { get; set; }

    public static implicit operator Result<T>(T data)
        => new()
        {
            Succeeded = true,
            Data = data
        };

    public static implicit operator Result<T>(System.Exception error)
        => new()
        {
            Succeeded = false,
            Error = error
        };

    public TMatch Match<TMatch>(Func<T, TMatch> onSuccess, Func<System.Exception, TMatch> onFail)
        => Succeeded ? onSuccess(Data) : onFail(Error);
}