using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Stove")]
public class StoveController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Stove);