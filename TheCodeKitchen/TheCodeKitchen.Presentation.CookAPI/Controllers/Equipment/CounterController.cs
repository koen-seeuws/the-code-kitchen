using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Counter")]
public class CounterController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Counter);