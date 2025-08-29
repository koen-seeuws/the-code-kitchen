using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Hot Plate")]
public class HotPlateController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.HotPlate);