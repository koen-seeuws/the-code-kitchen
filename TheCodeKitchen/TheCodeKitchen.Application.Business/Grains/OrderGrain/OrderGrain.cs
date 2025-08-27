using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain(
    [PersistentState(TheCodeKitchenState.Orders, TheCodeKitchenState.Orders)]
    IPersistentState<Order> state,
    IMapper mapper
) : Grain, IOrderGrain;