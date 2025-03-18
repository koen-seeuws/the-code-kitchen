using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen
{
    public void CloseRegistrations()
    {
        if (Code is null)
            throw new KitchenRegistrationsAlreadyClosedException($"Kitchen with id {Id}'s registrations are already closed");

        Code = null;
    }
}