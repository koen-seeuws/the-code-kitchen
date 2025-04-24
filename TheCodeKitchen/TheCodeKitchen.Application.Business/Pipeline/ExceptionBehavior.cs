using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Pipeline;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
    where TResponse : notnull
{
    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            return e;
        }
    }
}