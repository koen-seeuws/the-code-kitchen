using System.Reflection;

namespace TheCodeKitchen.Core.Shared.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(MemberInfo typeThatHasNotBeenFound) : base($"{typeThatHasNotBeenFound.Name} not found") { }
}

public class NotFoundException<T> : NotFoundException
{
    public NotFoundException() : base(typeof(T)) { }
}

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) : base(message) { }

    public ForbiddenException(MemberInfo typeThatIsForbidden) : base($"{typeThatIsForbidden.Name} action is forbidden.") { }
}

public class ForbiddenException<T> : ForbiddenException
{
    public ForbiddenException() : base(typeof(T)) { }
}
