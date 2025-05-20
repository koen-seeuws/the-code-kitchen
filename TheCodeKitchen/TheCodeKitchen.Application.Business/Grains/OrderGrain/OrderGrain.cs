using TheCodeKitchen.Application.Contracts.Contants;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public partial class OrderGrain(
    [PersistentState(TheCodeKitchenStorage.Order, TheCodeKitchenStorage.Order)]
    IPersistentState<Order> state,
    IMapper mapper
) : Grain, IOrderGrain;