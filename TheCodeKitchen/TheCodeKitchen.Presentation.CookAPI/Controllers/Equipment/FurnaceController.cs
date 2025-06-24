using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

public class FurnaceController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Furnace);