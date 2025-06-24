namespace TheCodeKitchen.Application.Business.Extensions;

public static class ResultExtensions
{
    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, Task<Result<TOut>>> next
    )
    {
        var result = await resultTask;
        if (!result.Succeeded)
            return result.Error;
        return await next(result.Value);
    }
}