namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    protected abstract Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _);
}