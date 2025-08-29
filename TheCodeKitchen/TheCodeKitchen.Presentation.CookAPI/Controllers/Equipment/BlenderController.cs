using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Blender")]
public class BlenderController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Blender);