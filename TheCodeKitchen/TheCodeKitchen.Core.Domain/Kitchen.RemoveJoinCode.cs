using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen
{
    public void RemoveJoinCode()
    {
        Code = null;
    }
}