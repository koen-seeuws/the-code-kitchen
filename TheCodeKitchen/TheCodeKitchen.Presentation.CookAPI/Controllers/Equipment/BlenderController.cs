using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Blender")]
public class BlenderController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Blender);