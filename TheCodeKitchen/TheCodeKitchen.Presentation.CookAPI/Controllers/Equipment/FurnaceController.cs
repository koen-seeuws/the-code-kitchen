using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Furnace")]
public class FurnaceController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Furnace);