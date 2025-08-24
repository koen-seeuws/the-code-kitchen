using TheCodeKitchen.Application.Business.Helpers;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;
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
        
        // Equipment 
        var equipments = new Dictionary<string, int>
        {
            { EquipmentType.Bbq, 6 },
            { EquipmentType.Blender, 4 },
            { EquipmentType.Counter, 30 },
            { EquipmentType.CuttingBoard, 6 },
            { EquipmentType.Freezer, 12 },
            { EquipmentType.Fridge, 15 },
            { EquipmentType.Fryer, 4 },
            { EquipmentType.HotPlate, 10 },
            { EquipmentType.Mixer, 4 },
            { EquipmentType.Oven, 4 },
            { EquipmentType.Stove, 6 },
        };

        // State
        var kitchen = new Kitchen(id, name, codeResult.Value, request.GameId, equipments);
        state.State = kitchen;
        await state.WriteStateAsync();

        // Equipment 
        foreach (var equipment in state.State.Equipment)
        {
            for (var number = 0; number < equipment.Value; number++)
            {
                var equipmentGrainIdExtension = EquipmentGrainIdHelper.CreateId(equipment.Key, number);
                var equipmentGrain = GrainFactory.GetGrain<IEquipmentGrain>(id, equipmentGrainIdExtension);

                var createEquipmentRequest = new CreateEquipmentRequest(request.GameId,id, number);
                await equipmentGrain.Initialize(createEquipmentRequest);
            }
        }
        
        // Streams
        await SubscribeToNextMomentEvent();
        await SubscribeToNewOrderEvent();
        await SubscribeToKitchenOrderRatingUpdatedEvent();

        return mapper.Map<CreateKitchenResponse>(kitchen);
    }
}