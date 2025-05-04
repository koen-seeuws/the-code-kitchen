using TheCodeKitchen.Application.Contracts.Interfaces.Security;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain(
    [PersistentState("Kitchen")] IPersistentState<Kitchen> state,
    IMapper mapper,
    IPasswordHashingService passwordHashingService
) : Grain, IKitchenGrain;