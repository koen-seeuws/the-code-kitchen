using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Fridge")]
public class FridgeController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Fridge);