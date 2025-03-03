using System.Collections.Immutable;

namespace TheCodeKitchen.Core.Shared.Monads;

public class AggregateResultException : ApplicationException
{
    public ImmutableArray<Exception> Exceptions { get; }

    private AggregateResultException(IEnumerable<Exception> exceptions)
    {
        Exceptions = [..exceptions];
    }

    public AggregateResultException(IEnumerable<IFailure> failures) : this(failures.Select(failure => failure.Exception)) { }

    public AggregateResultException(Exception exception, params Exception[] exceptions) : this([exception, ..exceptions]) { }

    public AggregateResultException(IFailure failure, params IFailure[] failures) : this([failure, ..failures]) { }
}
