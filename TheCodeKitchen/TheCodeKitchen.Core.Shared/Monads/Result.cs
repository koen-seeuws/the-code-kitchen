namespace TheCodeKitchen.Core.Shared.Monads;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
}

public interface ISuccess<out TValue> : IResult
{
    public TValue GetValue();
}

public interface IFailure : IResult
{
    public Exception Exception { get; }
}

public abstract partial class Result<TValue> : IResult
{
    public abstract bool IsSuccess { get; }
    public abstract bool IsFailure { get; }
    protected abstract TValue Value { get; }
    public static implicit operator Result<TValue>(TValue value) => new Success<TValue>(value);
    public static implicit operator Result<TValue>(Exception exception) => new Failure<TValue>(exception);
}

public abstract partial class Result<TValue>
{
    /// <summary>
    /// Be carefull with this... not really safe because it will throw when result is of type failure!!
    /// </summary>
    /// <param name="result"></param>
    public static explicit operator TValue(Result<TValue> result) => result.Match(value => value, failure => throw failure.Exception);
}


public sealed class Success<TValue> : Result<TValue>, ISuccess<TValue>
{
    public Success(TValue value) => Value = value;
    public override bool IsSuccess => true;
    public override bool IsFailure => false;
    protected override TValue Value { get; }
    public TValue GetValue() => Value;
}

public sealed class Failure<TValue> : Result<TValue>, IFailure
{
    public Failure(Exception exception) => Exception = exception; 
    public override bool IsSuccess => false;
    public override bool IsFailure => true;
    protected override TValue Value => default!;
    public Exception Exception { get; }
}

/// <summary>
/// If you need to combine more than 3 results ... implement your own :)
/// </summary>
public static class Result
{
    public static Result<TOut> Combine<TIn1, TIn2, TOut>(Result<TIn1> result1, Result<TIn2> result2, Func<TIn1, TIn2, TOut> selector)
        => (result1, result2) switch
        {
            (ISuccess<TIn1> success1, ISuccess<TIn2> success2) => selector(success1.GetValue(), success2.GetValue()),
            (IFailure failure1, IFailure failure2) => new AggregateResultException(failure1, failure2),
            (IFailure failure1, _) => failure1.Exception,
            (_, IFailure failure2) => failure2.Exception,
            _ => new ImpossibleResultCombinationOutOfRangeException()
        };
    
    public static Result<TOut> Combine<TIn1, TIn2, TIn3, TOut>(Result<TIn1> result1, Result<TIn2> result2, Result<TIn3> result3, Func<TIn1, TIn2, TIn3, TOut> selector)
        => (result1, result2, result3) switch
        {
            (ISuccess<TIn1> success1, ISuccess<TIn2> success2, ISuccess<TIn3> success3) => selector(success1.GetValue(), success2.GetValue(), success3.GetValue()),
            (IFailure failure1, IFailure failure2, IFailure failure3) => new AggregateResultException(failure1, failure2, failure3),
            (IFailure failure1, IFailure failure2, _) => new AggregateResultException(failure1, failure2),
            (IFailure failure1, _, IFailure failure3) => new AggregateResultException(failure1, failure3),
            (_, IFailure failure2, IFailure failure3) => new AggregateResultException(failure2, failure3),
            (IFailure failure1, _, _) => failure1.Exception,
            (_, IFailure failure2, _) => failure2.Exception,
            (_, _, IFailure failure3) => failure3.Exception,
            _ => new ImpossibleResultCombinationOutOfRangeException()
        };
}
