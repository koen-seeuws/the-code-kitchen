using Orleans;

namespace TheCodeKitchen.Application.Business.Interceptor;

public class ValidationInterceptor(IEnumerable<IValidator> validators) : IIncomingGrainCallFilter
{
    public Task Invoke(IIncomingGrainCallContext context)
    {
        throw new NotImplementedException();
    }
}