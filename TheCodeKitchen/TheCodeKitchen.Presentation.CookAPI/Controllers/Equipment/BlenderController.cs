using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

public class BlenderController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Blender);