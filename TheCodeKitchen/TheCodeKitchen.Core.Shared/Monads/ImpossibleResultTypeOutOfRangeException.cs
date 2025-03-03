namespace TheCodeKitchen.Core.Shared.Monads;

/// <summary>
/// Because C# does not support exhaustive pattern matching.
/// </summary>
internal class ImpossibleResultTypeOutOfRangeException : ArgumentOutOfRangeException
{
    internal ImpossibleResultTypeOutOfRangeException(Type typeofResult) 
        : base(typeofResult.Name, "Result is not of type IFailure or ISuccess, which should not be possible.") { }
    
    internal ImpossibleResultTypeOutOfRangeException(IResult result) 
        : this(result.GetType()) { }
}
