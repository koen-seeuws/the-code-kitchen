using TheCodeKitchen.Application.Contracts.Grains.Equipment;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class EquipmentGrain
{
    public async Task<Result<CreateKitchenResponse>> Initialize(CreateKitchenRequest request, int count)
    {
        if (state.RecordExists)
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
        foreach (var equipment in state.State.Equipment)
        {
            for (var number = 0; number < equipment.Value; number++)
            {
                var equipmentGrain = GrainFactory.GetGrain<IEquipmentGrain>(state.State.Id, number.ToString(), equipment.Key);
                var createEquipmentRequest = new CreateEquipmentRequest(state.State.Game, state.State.Id, number);
                await equipmentGrain.Initialize(createEquipmentRequest);
            }
        }
        
        // Streams
        await SubscribeToNextMomentEvent();

        return mapper.Map<CreateKitchenResponse>(kitchen);
    }
}