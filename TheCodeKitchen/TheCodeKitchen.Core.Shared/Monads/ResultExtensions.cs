namespace TheCodeKitchen.Core.Shared.Monads;

public static class ResultExtensions
{
    public static Result<TValue> ToResult<TValue>(this TValue value)
        => new Success<TValue>(value);

    public static async Task<Result<TValue>> ToResultAsync<TValue>(this Task<TValue> value)
        => new Success<TValue>(await value);

    public static Success<TValue> ToSuccess<TValue>(this TValue value)
        => new(value);

    public static Result<TValue?> ToNullable<TValue>(this Result<TValue> result)
        => result switch
        {
            ISuccess<TValue> success => success.GetValue().ToSuccess<TValue?>(),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    public static Failure<TValue> ToFailure<TValue>(this Exception exception)
        => new(exception);

    internal static bool TryGetException<TValue>(this Result<TValue> result, out Exception exception)
    {
        exception = default!;

        if (result is not IFailure failure) return false;

        exception = failure.Exception;

        return true;
    }

    public static bool TryGetFailure<TValue>(this Result<TValue> result, out IFailure failure)
    {
        failure = default!;

        if (result is not IFailure f) return false;

        failure = f;

        return true;
    }

    internal static bool TryGetValue<TValue>(this Result<TValue> result, out TValue value)
    {
        value = default!;

        if (result is not ISuccess<TValue> success) return false;

        value = success.GetValue();

        return true;
    }
    
    public static TheCodeKitchenUnit IfFailure<TValue>(this Result<TValue> result, Action<Exception> whenFailure)
    {
        if (result is IFailure failure)
        {
            whenFailure(failure.Exception);
        }

        return TheCodeKitchenUnit.Value;
    }

    public static TheCodeKitchenUnit IfSuccess<TValue>(this Result<TValue> result, Action<TValue> whenSuccess)
    {
        if (result is ISuccess<TValue> success)
        {
            whenSuccess(success.GetValue());
        }

        return TheCodeKitchenUnit.Value;
    }

    public static Result<TOut> Convert<TOut>(this IFailure failure)
        => new Failure<TOut>(failure.Exception);

    public static Result<TValue> Convert<TValue>(this IEnumerable<IFailure> failures)
    {
        IFailure[] failureArray = [.. failures];

        return failureArray.Length == 1
            ? failureArray[0].Convert<TValue>()
            : new AggregateResultException(failureArray).ToFailure<TValue>();
    }

    internal static Result<T> Flatten<T>(this Result<Result<T>> result)
        => result switch
        {
            ISuccess<Result<T>> success => success.GetValue(),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    internal static async Task<Result<TValue>> FlattenAsync<TValue>(this Task<Result<Result<TValue>>> resultTask)
        => await resultTask switch
        {
            ISuccess<Result<TValue>> success => success.GetValue(),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static TValue GetValueOrThrow<TValue>(this Result<TValue> result)
        => result.Match(value => value, failure => throw failure.Exception);

    public static async Task<TValue> GetValueOrThrowAsync<TValue>(this Task<Result<TValue>> result)
        => await result switch
        {
            ISuccess<TValue> success => success.GetValue(),
            IFailure failure => throw failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result.GetType())
        };

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> whenSuccess)
        => result switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> whenSuccess)
        => result switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> whenSuccess)
        => result switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<TOut>> whenSuccess)
        => result switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(result)
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result<TOut>>> whenSuccess)
        => await resultTask switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<TOut>> whenSuccess)
        => await resultTask switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> whenSuccess)
        => await resultTask switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Result<TOut>> whenSuccess)
        => await resultTask switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    /// <summary>
    /// Can be used to trigger a side-effect, thus not influencing the actual success value.
    /// </summary>
    /// <param name="resultTask"></param>
    /// <param name="whenSuccess"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    /// <exception cref="ImpossibleResultTypeOutOfRangeException"></exception>
    public static async Task<Result<TValue>> TapAsync<TValue>(this Task<Result<TValue>> resultTask, Action<TValue> whenSuccess)
    {
        var result = await resultTask;

        result.IfSuccess(whenSuccess);

        return result;
    }

    /// <summary>
    /// Can be used to trigger a side-effect, thus not influencing the actual success value.
    /// </summary>
    /// <param name="resultTask"></param>
    /// <param name="whenSuccess"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    /// <exception cref="ImpossibleResultTypeOutOfRangeException"></exception>
    public static async Task<Result<TValue>> TapAsync<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task> whenSuccess)
        => await resultTask switch
        {
            ISuccess<TValue> success => await whenSuccess(success.GetValue()).ContinueWith(_ => success.GetValue()),
            IFailure failure => failure.Exception,
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static TOut Match<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> whenSuccess, Func<IFailure, TOut> whenFailure)
        => result switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => whenFailure(failure),
            _ => throw new ImpossibleResultTypeOutOfRangeException(result.GetType())
        };

    public static async Task<TOut> MatchAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<TOut>> whenSuccess, Func<IFailure, Task<TOut>> whenFailure)
        => result switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => await whenFailure(failure),
            _ => throw new ImpossibleResultTypeOutOfRangeException(result.GetType())
        };

    public static TheCodeKitchenUnit Match<TIn>(this Result<TIn> result, Action<TIn> whenSuccess, Action<IFailure> whenFailure)
        => result.Match(
            value =>
            {
                whenSuccess(value);
                return TheCodeKitchenUnit.Value;
            },
            failure =>
            {
                whenFailure(failure);
                return TheCodeKitchenUnit.Value;
            }
        );

    public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> whenSuccess, Func<IFailure, TOut> whenFailure)
        => await resultTask switch
        {
            ISuccess<TIn> success => whenSuccess(success.GetValue()),
            IFailure failure => whenFailure(failure),
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static async Task<TOut> MatchAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<TOut>> whenSuccess, Func<IFailure, Task<TOut>> whenFailure)
        => await resultTask switch
        {
            ISuccess<TIn> success => await whenSuccess(success.GetValue()),
            IFailure failure => await whenFailure(failure),
            _ => throw new ImpossibleResultTypeOutOfRangeException(resultTask.GetType())
        };

    public static async Task<TheCodeKitchenUnit> MatchAsync<TIn>(this Task<Result<TIn>> resultTask, Func<TIn, Task> whenSuccess, Func<IFailure, Task> whenFailure)
        => await resultTask.MatchAsync(
            async value =>
            {
                await whenSuccess(value);
                return TheCodeKitchenUnit.Value;
            },
            async failure =>
            {
                await whenFailure(failure);
                return TheCodeKitchenUnit.Value;
            }
        );

    public static async Task<TheCodeKitchenUnit> MatchAsync<TIn>(this Task<Result<TIn>> resultTask, Action<TIn> whenSuccess, Action<IFailure> whenFailure)
        => await resultTask.MatchAsync(
            async value =>
            {
                whenSuccess(value);
                return await Task.FromResult(TheCodeKitchenUnit.Value);
            },
            async failure =>
            {
                whenFailure(failure);
                return await Task.FromResult(TheCodeKitchenUnit.Value);
            }
        );

    public static IEnumerable<Result<TOut>> Map<TIn, TOut>(this IEnumerable<Result<TIn>> results, Func<TIn, Result<TOut>> whenSuccess)
        => results.Select(result => result.Map(whenSuccess));

    public static IEnumerable<Result<TOut>> Map<TIn, TOut>(this IEnumerable<Result<TIn>> results, Func<TIn, TOut> whenSuccess)
        => results.Select(result => result.Map(whenSuccess));

    public static Result<TOut> Reduce<TIn, TOut>(this IEnumerable<TIn> source, Result<TOut> seed, Func<Result<TOut>, TIn, Result<TOut>> whenSuccess)
    {
        var result = seed;

        foreach (var item in source)
        {
            if (result.IsFailure) break;

            result = whenSuccess(result, item);
        }

        return result;
    }


    public static async Task<Result<TOut>> ReduceAsync<TIn, TOut>(this IEnumerable<TIn> source, Result<TOut> seed, Func<TOut, TIn, Task<Result<TOut>>> whenSuccess)
    {
        var result = seed;

        foreach (var item in source)
        {
            if (result.IsFailure) break;

            result = await result.MapAsync(value => whenSuccess(value, item));
        }

        return result;
    }

    public static Result<IEnumerable<TOut>> MapSeq<TIn, TOut>(this Result<IEnumerable<TIn>> result, Func<TIn, Result<TOut>> transformer)
        => result.Map(sequence => sequence.MapSeq(transformer));

    public static Result<IEnumerable<TOut>> MapSeq<TIn, TOut>(this IEnumerable<TIn> sequence, Func<TIn, Result<TOut>> transformer)
    {
        try
        {
            var list = new List<TOut>();
            foreach (var item in sequence)
            {
                var result = transformer(item);

                switch (result)
                {
                    case ISuccess<TOut> success:
                        list.Add(success.GetValue());
                        continue;
                    case IFailure failure:
                        return failure.Exception;
                }
            }

            return list;
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    public static async Task<Result<IEnumerable<TOut>>> MapSeqAsync<TIn, TOut>(this IEnumerable<TIn> sequence, Func<TIn, Task<Result<TOut>>> transformer)
    {
        try
        {
            var list = new List<TOut>();
            foreach (var item in sequence)
            {
                var result = await transformer(item);

                switch (result)
                {
                    case ISuccess<TOut> success:
                        list.Add(success.GetValue());
                        continue;
                    case IFailure failure:
                        return failure.Exception;
                }
            }

            return list;
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    public static Result<IEnumerable<TItem>> Filter<TItem>(this IEnumerable<TItem> source, Func<TItem, Result<bool>> predicate)
    {
        try
        {
            var list = new List<TItem>();

            foreach (var item in source)
            {
                var result = predicate(item);

                switch (result)
                {
                    case ISuccess<bool> success when success.GetValue():
                        list.Add(item);
                        continue;
                    case IFailure failure:
                        return failure.Exception;
                }
            }

            return list;
        }
        catch(Exception exception)
        {
            return exception;
        }
    }

    internal static IEnumerable<ISuccess<TValue>> GetSuccesses<TValue>(this IEnumerable<Result<TValue>> results)
    {
        foreach (var result in results)
        {
            if (result is ISuccess<TValue> success) yield return success;
        }
    }

    public static IEnumerable<TValue> GetSuccessValues<TValue>(this IEnumerable<Result<TValue>> results)
    {
        foreach (var result in results)
        {
            if (result is Success<TValue> success) yield return success.GetValue();
        }
    }

    internal static IEnumerable<IFailure> GetFailures(this IEnumerable<IResult> results)
    {
        foreach (var result in results)
        {
            if (result is IFailure failure) yield return failure;
        }
    }

    public static IEnumerable<IFailure> GetFailures<TValue>(this IEnumerable<Result<TValue>> results)
    {
        foreach (var result in results)
        {
            if (result is IFailure failure) yield return failure;
        }
    }

    internal static IEnumerable<Exception> GetFailureValues<TValue>(this IEnumerable<Result<TValue>> results)
    {
        foreach (var result in results)
        {
            if (result is IFailure failure) yield return failure.Exception;
        }
    }

    internal static IEnumerable<TException> GetFailureValuesOfType<TException>(this IEnumerable<IResult> results) where TException : Exception
    {
        foreach (var result in results)
        {
            if (result is IFailure { Exception: TException exception }) yield return exception;
        }
    }

    internal static void ForEachFailure(this IEnumerable<IResult> results, Action<IFailure> action)
    {
        foreach (var result in results)
        {
            if (result is IFailure failure)
            {
                action(failure);
            }
        }
    }

    internal static async Task ForEachFailureAsync(this IEnumerable<IResult> results, Func<IFailure, Task> task)
    {
        foreach (var result in results)
        {
            if (result is IFailure failure)
            {
                await task(failure);
            }
        }
    }
}
