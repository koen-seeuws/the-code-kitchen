using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Oven")]
public class OvenController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Oven);