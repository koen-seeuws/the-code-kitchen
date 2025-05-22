namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain(
    [PersistentState(TheCodeKitchenStorage.Order, TheCodeKitchenStorage.Order)]
    IPersistentState<Order> state,
    IMapper mapper
) : Grain, IOrderGrain;