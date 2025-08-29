using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Fryer")]
public class FryerController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Fryer);