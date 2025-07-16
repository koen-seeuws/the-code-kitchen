using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain(
    [PersistentState(TheCodeKitchenState.Orders, TheCodeKitchenState.Orders)]
    IPersistentState<Order> state,
    IMapper mapper,
    ILogger<OrderGrain> logger
) : Grain, IOrderGrain;