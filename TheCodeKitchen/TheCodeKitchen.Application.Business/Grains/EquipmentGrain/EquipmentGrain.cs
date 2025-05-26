namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public class EquipmentGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public abstract partial class EquipmentGrain(
    IPersistentState<Equipment> state,
    IPersistentState<EquipmentGrainStreamSubscriptionHandles> streamSubscriptionSubscriptionHAndleses,
    IMapper mapper,
    short maxItems
) : Grain;