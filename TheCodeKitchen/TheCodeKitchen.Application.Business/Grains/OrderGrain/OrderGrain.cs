namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain(
    [PersistentState(TheCodeKitchenState.Order, TheCodeKitchenState.Order)]
    IPersistentState<Order> state,
    IMapper mapper
) : Grain, IOrderGrain;