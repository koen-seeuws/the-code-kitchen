using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class EquipmentGrain
{
    public async Task<Result<CreateKitchenResponse>> Initialize(CreateKitchenRequest request, int count)
    {
        if(state.RecordExists)
            return new AlreadyExistsError($"The kitchen with id {this.GetPrimaryKey()} has already been initialized");
        
        var id = this.GetPrimaryKey();

        var name = request.Name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Kitchen {count}";

        var kitchenCodeIndexGrain = GrainFactory.GetGrain<IKitchenManagementGrain>(Guid.Empty);
        var codeResult = await kitchenCodeIndexGrain.GenerateUniqueCode(id);

        if (!codeResult.Succeeded)
            return codeResult.Error;
        
        // State
        var kitchen = new Kitchen(id, name, codeResult.Value, request.GameId);
        state.State = kitchen;
        await state.WriteStateAsync();
        
        // Equipment 
        
        
        // Streams
        await SubscribeToNextMomentEvent();
        
        return mapper.Map<CreateKitchenResponse>(kitchen);
    }
}