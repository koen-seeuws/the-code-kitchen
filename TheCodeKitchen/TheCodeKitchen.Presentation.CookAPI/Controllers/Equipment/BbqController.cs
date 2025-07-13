using Microsoft.AspNetCore.Mvc;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - BBQ")]
[Route("Equipment/BBQ")]
public class BbqController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Bbq);