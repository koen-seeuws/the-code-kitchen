using TheCodeKitchen.Application.Contracts.Grains.Equipment;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
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
                IEquipmentGrain equipmentGrain = equipment.Key switch
                {
                    EquipmentTypes.Blender => GrainFactory.GetGrain<IBlenderGrain>(state.State.Id, number.ToString()),
                    EquipmentTypes.Furnace => GrainFactory.GetGrain<IFurnaceGrain>(state.State.Id, number.ToString()),
                    _ => throw new ArgumentOutOfRangeException()
                };

                var createEquipmentRequest = new CreateEquipmentRequest(state.State.Game, state.State.Id, number);
                await equipmentGrain.Initialize(createEquipmentRequest);
            }
        }
        
        // Streams
        await SubscribeToNextMomentEvent();
        await SubscribeToNewOrderEvent();

        return mapper.Map<CreateKitchenResponse>(kitchen);
    }
}