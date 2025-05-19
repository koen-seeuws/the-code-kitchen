namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ReleaseJoinCode()
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");

        if (string.IsNullOrWhiteSpace(state.State.Code))
            return new EmptyError();

        var kitchenCodeIndexGrain = GrainFactory.GetGrain<IKitchenManagementGrain>(Guid.Empty);
        await kitchenCodeIndexGrain.DeleteKitchenCode(state.State.Code);

        state.State.Code = null;
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}